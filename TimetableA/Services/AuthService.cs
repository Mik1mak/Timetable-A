﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimetableA.API.Helpers;
using TimetableA.API.Models.InputModels;
using TimetableA.API.Models.OutputModels;
using TimetableA.DataAccessLayer.Repositories.Abstract;
using TimetableA.Entities.Models;

namespace TimetableA.API.Services
{
    public interface IAuthService
    {
        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }

    public class AuthService : IAuthService
    {
        private readonly ITimetableRepository timetables;

        public AuthService(ITimetableRepository repository)
        {
            timetables = repository;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            Timetable timetable = await timetables.GetAsync(model.Id);

            if (timetable == default)
                return null;

            string token;
            AuthLevel authLvl;

            if (timetable.ReadKey == model.Key)
            {
                authLvl = AuthLevel.Read;
                timetable.EditKey = default;
                token = GenerateJwtToken(timetable, model.Key);
            }
            else if(timetable.EditKey == model.Key)
            {
                authLvl = AuthLevel.Edit;
                timetable.ReadKey = default;
                token = GenerateJwtToken(timetable, model.Key);
            }
            else
            {
                return null;
            }

            return new AuthenticateResponse(timetable, token, authLvl);
        }

        private string GenerateJwtToken(Timetable timetable, string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyJWT = Encoding.ASCII.GetBytes("super sekretny kluczyk"); //TODO secret key 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("id", timetable.Id.ToString()),
                    new Claim("key", key),
                }),
                Expires = DateTime.UtcNow.AddMonths(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyJWT), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
