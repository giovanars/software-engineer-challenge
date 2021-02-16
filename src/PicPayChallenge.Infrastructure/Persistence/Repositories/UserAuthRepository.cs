using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.Entities;
using PicPayChallenge.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PicPayChallenge.Infrastructure.Persistence.Repositories
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly List<UserAuth> userAuths;
        public UserAuthRepository()
        {
            userAuths = new List<UserAuth> 
            {
                new UserAuth { Identifier = "Teste1", Secret = "Teste1"},
                new UserAuth { Identifier = "Teste2", Secret = "Teste2"}
            };
        }

        public UserAuth GetUserAuthByCredentials(AuthRequestDTO request)
        {
            //TODO: Implementar busca no banco
            return userAuths.FirstOrDefault(u => u.Identifier == request.Identifier && u.Secret == request.Secret);
        }
    }
}
