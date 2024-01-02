using BookAPI.Models.DTOs;
using Library.Core.DTOs;
using Library.Core.Result;
using Library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Result<ReturnUserDto>> Register( CreateUserDto model);
        Task<SignInResult> Login(User user, string password);

    }
}
