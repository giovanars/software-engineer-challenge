using PicPayChallenge.Application.Interfaces;
using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.Entities;
using PicPayChallenge.Core.Interfaces.Repositories;
using PicPayChallenge.Core.Interfaces.Services;

namespace PicPayChallenge.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserAuthRepository userAuthRepository;
        private readonly IJtwService jtwService;

        public AuthService(IUserAuthRepository userAuthRepository, IJtwService jtwService)
        {
            this.userAuthRepository = userAuthRepository;
            this.jtwService = jtwService;
        }

        public UserAuth GetUserAuthByCredentials(AuthRequestDTO request) => userAuthRepository.GetUserAuthByCredentials(request);

        public string GenerateToken(AuthRequestDTO request) => jtwService.GenerateToken(request);

    }
}
