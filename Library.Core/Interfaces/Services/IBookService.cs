using Library.Core.DTOs;
using Library.Core.Result;

namespace Library.Core.Interfaces.Services;

public interface IBookService
{
    public Task<Result<PaginatorResponseDto<IEnumerable<GetAllBooksDto>>>> GetAll(int pageSize, int pageNumber);

    public Task<Result<BookDetailsDto>> GetBookById(string id);
}