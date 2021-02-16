using PicPayChallenge.Core.DTOs;

namespace PicPayChallenge.Core.Interfaces.Services
{
    public interface IJtwService
    {
        string GenerateToken(AuthRequestDTO request);
    }
}
