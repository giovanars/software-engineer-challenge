using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicPayChallenge.Application.Interfaces;
using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.ErrorCodes;
using PicPayChallenge.Core.Exceptions;
using System.Collections.Generic;

namespace PicPayChallenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService user;
        private readonly IMapper mapper;

        public UsersController(IUserService user, IMapper mapper)
        {
            this.user = user;
            this.mapper = mapper;
        }

        [HttpGet, Route("{term}/{page?}/{rowsPage?}")]
        public IActionResult Get(string term, int page = 1, int rowsPage = 15)
        {
            if (string.IsNullOrEmpty(term) || term.Length + 1 < 3)
                throw new ValidationException(ErrorCodes.InvalidaRequestObject);

            return Ok(mapper.Map<IEnumerable<UserResponseDTO>>(user.GetUsersByTerm(new UserRequestDTO { Term = term, PageNumber = page, RowsOfPage = rowsPage })));
        }
    }
}