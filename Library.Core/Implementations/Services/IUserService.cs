using BookAPI.Models.DTOs;
using Library.Core.DTOs;
using Library.Core.Result;

namespace Library.Core.Implementations.Services
{
    public interface IUserService
    {
        Task<Result<PaginatorResponseDto<IEnumerable<AppUserDto>>>> SearchUsers(string keyword, int pageSize, int pageNumber);
    }
}