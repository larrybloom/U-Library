using Library.Core.DTOs;
using Library.Core.Interfaces.Services;
using System;
using Library.Domain.Models;
using Library.Core.Implementations.Services;
using Microsoft.AspNetCore.Identity;
using Library.Core.Result;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BookAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Library.Core.Implementations.Services
{
    public class AuthService : IAuthService
    { 
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(IConfiguration config, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string GenerateJWT(User user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName}{user.LastName}"));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Token").Value);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var jwtsecuritytokenhandler = new JwtSecurityTokenHandler();

            var securityToken = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToInt32(_config.GetSection("Jwt:LifeSpan").Value)),
                signingCredentials: signingCredentials
            );

            var token = jwtsecuritytokenhandler.WriteToken(securityToken);

            return token;
        }

        public async Task<Result<ReturnUserDto>> Register(CreateUserDto model)
        {

            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = $"{model.LastName}{new string(model.Email.Reverse().ToArray())}",
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                var userToReturn = new ReturnUserDto
                {
                    Id = newUser.Id,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    PhoneNumber = newUser.PhoneNumber,
                    Username = newUser.UserName,
                };

                return Result<ReturnUserDto>.Success(userToReturn);
            }
            else
            {
                return Result<ReturnUserDto>.Fail(new[] {"user registration failed"});
            }
        }


        public async Task<SignInResult> Login(User user, string password)
        {
            
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }

    
    }

}
