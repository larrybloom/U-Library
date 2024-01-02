using BookAPI.Models.DTOs;
using Library.API.Responses;
using Library.Core.DTOs;
using Library.Core.Interfaces.Services;
using Library.Domain.Models;
using Library.Core.Implementations.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthService authService, UserManager<User> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost("register")]
            public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto model)
            {
                if (!ModelState.IsValid)
                {
                return BadRequest(ModelState);
                }

                var serviceResult = await _authService.Register(model);

                if (serviceResult.IsSuccessful)
                {
                    return Ok(ResponseDTO<ReturnUserDto>.Success(serviceResult.Data!, "New User Added Successfully"));
                }
                else
                {
                    return BadRequest(ResponseDTO<User>.Fail(serviceResult.Errors));
                }
            }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
         {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

             if (user == null)
                 return BadRequest(ResponseDTO<string>.Fail(new[] {"User not found"}));
             

             var result = await _authService.Login(user, model.Password);

             if (!result.Succeeded)
                return BadRequest(ResponseDTO<string>.Fail(new[] { "Invalid Login Credentials" }));

            return Ok(ResponseDTO<string>.Success("Login Successfull"));

        }


    }
}
