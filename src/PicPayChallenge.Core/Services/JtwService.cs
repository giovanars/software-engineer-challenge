using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PicPayChallenge.Core.Configs;
using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.Interfaces.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PicPayChallenge.Core.Services
{
    public class JtwService : IJtwService
    {
        private readonly string secret;
        public JtwService(IOptions<JwtConfig> options)
        {
            secret = options.Value.Secret;
        }

        public string GenerateToken(AuthRequestDTO request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, request.Identifier),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
