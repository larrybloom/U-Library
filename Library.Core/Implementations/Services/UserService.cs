using BookAPI.Models.DTOs;
using Library.Core.DTOs;
using Library.Core.Result;
using Library.Core.Utilities;
using Library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }



        public async Task<Result<PaginatorResponseDto<IEnumerable<AppUserDto>>>> SearchUsers(string keyword, int pageSize, int pageNumber)
        {
            var users = _userManager.Users
            .Where(user => user.FirstName.Contains(keyword) || user.LastName.Contains(keyword) || user.Email!.Contains(keyword))
            .OrderByDescending(user => user.FirstName)
            .Select(user => new AppUserDto
            {
                ID = user.Id,
                Name = user.FirstName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber

            });

            var paginatedUsers = await users.PaginationAsync<AppUserDto>(pageSize, pageNumber);

            return Result<PaginatorResponseDto<IEnumerable<AppUserDto>>>.Success(paginatedUsers);

        }

    }
}
