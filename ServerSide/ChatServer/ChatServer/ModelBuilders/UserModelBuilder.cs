using AutoMapper;
using System.Linq;
using ChatServer.DTOs;
using ChatServer.Helpers;
using ChatServer.Services;
using ChatServer.Models.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.ModelBuilders
{
    public static class UserModelBuilder
    {
        internal static void Register(IBaseService BaseService, IMapper Mapper, UserDto UserDto)
        {
            User User = Mapper.Map<User>(UserDto);
            if (string.IsNullOrWhiteSpace(UserDto.Password)) throw new AppException("Password is required.");
            if (!UserDto.Password.Equals(UserDto.ConfirmPassword)) throw new AppException("Passwords do not match");
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
        internal static UserDto Authenticate(IBaseService BaseService, IMapper Mapper, UserDto UserDto, AppSettings AppSettings)
        {
            var user = BaseService.Authenticate(UserDto.Username, UserDto.Password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            };
        }
    }
}
