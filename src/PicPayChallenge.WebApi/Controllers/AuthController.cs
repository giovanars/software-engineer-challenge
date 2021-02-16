using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicPayChallenge.Application.Interfaces;
using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.ErrorCodes;
using PicPayChallenge.Core.Exceptions;
using PicPayChallenge.Core.Interfaces.Repositories;
using PicPayChallenge.Core.Interfaces.Services;

namespace PicPayChallenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]AuthRequestDTO request)
        {
            var userAuth = authService.GetUserAuthByCredentials(request);

            if (userAuth == null)
                throw new ValidationException(ErrorCodes.UserNotFound);

            var token = authService.GenerateToken(request);

            return Ok(new { User = request.Identifier, Token = token });
        }
    }
}