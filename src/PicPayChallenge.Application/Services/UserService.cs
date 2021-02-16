using PicPayChallenge.Application.Interfaces;
using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.Entities;
using PicPayChallenge.Core.Interfaces.Repositories;
using System.Collections.Generic;

namespace PicPayChallenge.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<User> GetUsersByTerm(UserRequestDTO request) => userRepository.GetUsersByTerm(request);
    }
}
