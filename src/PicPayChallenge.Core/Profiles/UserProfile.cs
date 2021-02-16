using AutoMapper;
using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.Entities;

namespace PicPayChallenge.Core.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDTO>();
        }
    }
}
