using System;
using AutoMapper;
using System.Text;
using ChatServer.DTOs;
using ChatServer.Helpers;
using ChatServer.Services;
using System.Security.Claims;
using ChatServer.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using ChatServer.ModelBuilders;

namespace ChatServer.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private IBaseService _baseService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IBaseService BaseService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _baseService = BaseService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var User = UserModelBuilder.Authenticate(_baseService, _mapper, userDto, _appSettings);
            if (User == null) return Unauthorized();
            return Ok(new { User.Id, User.Username, User.FirstName, User.LastName, User.Token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            try
            {
                UserModelBuilder.Register(_baseService, _mapper, userDto);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _baseService.GetUsers();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid Id)
        {
            var user = _baseService.GetUserById(Id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid Id, [FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = Id;

            try
            {
                _baseService.Update(user);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid Id)
        {
            _baseService.Delete(_baseService.GetUserById(Id));
            return Ok();
        }
    }
}
