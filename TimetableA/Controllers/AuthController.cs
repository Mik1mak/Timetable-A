using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableA.API.Models.InputModels;
using TimetableA.API.Services;

namespace TimetableA.API.Controllers
{
    [Route("Authenticate/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IAuthService authService;

        public AuthController(ILogger<TimetableController> logger, IAuthService authService)
        {
            this.logger = logger;
            this.authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await authService.Authenticate(model);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }
    }
}
