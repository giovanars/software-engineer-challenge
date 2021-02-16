using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.Entities;
using System.Collections.Generic;

namespace PicPayChallenge.Application.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetUsersByTerm(UserRequestDTO request);
    }
}
