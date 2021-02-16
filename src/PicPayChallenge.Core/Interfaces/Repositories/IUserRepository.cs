using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.Entities;
using System.Collections.Generic;

namespace PicPayChallenge.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsersByTerm(UserRequestDTO request);
    }
}
