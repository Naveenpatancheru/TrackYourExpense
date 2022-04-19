using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrackYourExpenseApi.Entities;
using TrackYourExpenseApi.Helpers;
using TrackYourExpenseApi.Models;
using TrackYourExpenseApi.Services;

namespace TrackYourExpenseApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UsersController(IUserAuthenticationRepository userAuthenticationRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userAuthenticationRepository = userAuthenticationRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _appSettings = appSettings.Value;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterUserDto model)
        {
            var user = _mapper.Map<User>(model);
            try
            {
                // create user
                _userAuthenticationRepository.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDto model)
        {
            var user = _userAuthenticationRepository.Authenticate(model.UserName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }
    }
}
