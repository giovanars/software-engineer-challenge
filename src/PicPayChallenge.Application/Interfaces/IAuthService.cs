using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.Entities;

namespace PicPayChallenge.Application.Interfaces
{
    public interface IAuthService
    {
        UserAuth GetUserAuthByCredentials(AuthRequestDTO request);
        string GenerateToken(AuthRequestDTO request);

    }
}
