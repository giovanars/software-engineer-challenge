using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.Entities;

namespace PicPayChallenge.Core.Interfaces.Repositories
{
    public interface IUserAuthRepository
    {
        UserAuth GetUserAuthByCredentials(AuthRequestDTO request);
    }
}
