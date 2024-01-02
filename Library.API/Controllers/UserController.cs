using BookAPI.Models.DTOs;
using Library.API.Responses;
using Library.Core.DTOs;
using Library.Core.Implementations.Services;
using Library.Core.Result;
using Library.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [AllowAnonymous]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers(string keyword, [FromQuery] int pageSize = 10, [FromQuery]int pageNumber = 1)
        {
            if (string.IsNullOrEmpty(keyword))
                return BadRequest(Result<PaginatorResponseDto<IEnumerable<AppUserDto>>>.Fail(new [] {"keyword must not be empty"}));

            var result = await _userService.SearchUsers(keyword, pageSize, pageNumber);

            if (!result.IsSuccessful) 
                return BadRequest(ResponseDTO<IEnumerable<string>>.Fail(result.Errors));
            return Ok(ResponseDTO<PaginatorResponseDto<IEnumerable<AppUserDto>>>.Success(result.Data!));
        }
    }
}
