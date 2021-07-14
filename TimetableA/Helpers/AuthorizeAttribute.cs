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
        public AuthorizeAttribute(AuthLevel minimumLevel)
        {
            this.minimumLevel = minimumLevel;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var key = (string)context.HttpContext.Items["Key"];
            var timetable = (Timetable)context.HttpContext.Items["Timetable"];
            var requestId = (string)context.HttpContext.Request.RouteValues["id"];

            bool isInvalid = false;

            if (timetable == null || string.IsNullOrEmpty(key))
                isInvalid = true;
            else if (minimumLevel > GetAuthLevel(timetable, key))
                isInvalid = true;
            else if(!string.IsNullOrEmpty(requestId))
            {
                if (requestId != timetable.Id.ToString())
                    isInvalid = true;
            }

            if(isInvalid)
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
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
