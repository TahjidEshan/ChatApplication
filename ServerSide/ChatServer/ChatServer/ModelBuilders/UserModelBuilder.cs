using AutoMapper;
using System.Linq;
using ChatServer.DTOs;
using ChatServer.Helpers;
using ChatServer.Services;
using ChatServer.Models.Data;

namespace ChatServer.ModelBuilders
{
    public static class UserModelBuilder
    {
        internal static void Register(IBaseService BaseService, IMapper Mapper, UserDto UserDto)
        {
            User User = Mapper.Map<User>(UserDto);
            if (string.IsNullOrWhiteSpace(UserDto.Password)) throw new AppException("Password is required.");
            if (BaseService.GetUsers().Any(x => x.UserName.Equals(User.UserName)))
                throw new AppException("The Username " + User.UserName + " is already taken, " +
                    "please choose another username.");
            if (BaseService.GetUsers().Any(x => x.EmailAddress.Equals(User.EmailAddress)))
                throw new AppException("The Email address" + User.EmailAddress + " is already taken.");
            byte[] PasswordHash, PasswordSalt;
            ServiceHelpers.CreatePasswordHash(UserDto.Password, out PasswordHash, out PasswordSalt);
            User.PasswordHash = PasswordHash;
            User.PasswordSalt = PasswordSalt;
            BaseService.Save(User);
        }
    }
}
