using Library.Core.DTOs;
using Library.Core.Interfaces.Repositories;
using Library.Core.Interfaces.Services;
using Library.Core.Result;

namespace Library.Core.Implementations.Services;

public class BookService: IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Result<PaginatorResponseDto<IEnumerable<GetAllBooksDto>>>> GetAll(int pageSize, int pageNumber)
    {
        return await _bookRepository.GetAll(pageSize, pageNumber);
    }

    public async Task<Result<BookDetailsDto>> GetBookById(string id)
    {
        return await _bookRepository.GetBookById(id);
    }
}