using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.Entities.Models;

namespace TimetableA.API.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly AuthLevel minimumLevel;
        private readonly IAuthValidationMethod authValMethod;
        private readonly string idKey;
        public AuthorizeAttribute(AuthLevel minimumLevel)
        {
            this.minimumLevel = minimumLevel;
            this.idKey = "id";
        }
        public AuthorizeAttribute(AuthLevel minimumLevel, Type AuthValidationMethod, string idKey = "id")
        {
            if (AuthValidationMethod.GetInterface(nameof(IAuthValidationMethod)) == null)
                throw new ArgumentException($"type of Auth Validation Method must implement {nameof(IAuthValidationMethod)}");

            this.minimumLevel = minimumLevel;
            this.authValMethod = (IAuthValidationMethod)Activator.CreateInstance(AuthValidationMethod);
            this.idKey = idKey;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var key = (string)context.HttpContext.Items["Key"];
            var timetable = (Timetable)context.HttpContext.Items["Timetable"];
            var requestId = (string)context.HttpContext.Request.RouteValues[idKey];

            bool isInvalid = false;

            if (timetable == null || string.IsNullOrEmpty(key))
                isInvalid = true;
            else if (minimumLevel > GetAuthLevel(timetable, key))
                isInvalid = true;

            if (!string.IsNullOrEmpty(requestId))
            {
                if (timetable == null)
                    isInvalid = true;
                else if (!authValMethod.Valid(requestId, timetable))
                {
                    context.Result = new JsonResult("Not Found") { StatusCode = StatusCodes.Status404NotFound };
                    isInvalid = false;
                }
            }
                
            if (isInvalid)
                context.Result = new JsonResult("Unauthorized") { StatusCode = StatusCodes.Status401Unauthorized };
        }

        private AuthLevel GetAuthLevel(Timetable timetable, string key)
        {
            if (timetable.ReadKey == key)
                return AuthLevel.Read;

            if (timetable.EditKey == key)
                return AuthLevel.Edit;

            return AuthLevel.Unauthorized;
        }
    }
}
