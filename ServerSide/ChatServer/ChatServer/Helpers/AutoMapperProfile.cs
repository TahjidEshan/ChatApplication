using AutoMapper;
using ChatServer.DTOs;
using ChatServer.Models.Data;

namespace ChatServer.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
