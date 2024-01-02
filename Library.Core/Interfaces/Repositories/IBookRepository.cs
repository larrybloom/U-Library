using Library.Core.DTOs;
using Library.Core.Result;

namespace Library.Core.Interfaces.Repositories;

public interface IBookRepository
{
    public Task<Result<PaginatorResponseDto<IEnumerable<GetAllBooksDto>>>> GetAll(int pageSize = 10,
        int pageNumber = 1);
    
    public Task<Result<BookDetailsDto>> GetBookById(string id);

}