using AutoMapper;
using Library.Core.DTOs;
using Library.Core.Interfaces.Repositories;
using Library.Core.Result;
using Library.Core.Utilities;
using Library.Domain.Models;
using Library.Infrastructure.Context;

namespace Library.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;

    public BookRepository(LibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatorResponseDto<IEnumerable<GetAllBooksDto>>>> GetAll(int pageSize = 10, int pageNumber = 1)
    {
        var books = _context.Books
            .OrderByDescending(book => book.CreatedAt)
            .Select(book => new GetAllBooksDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsAvailable = book.AvailableCopies > 0,
            });

        var paginatedBooks = await books.PaginationAsync<GetAllBooksDto>(pageSize, pageNumber);

        return Result<PaginatorResponseDto<IEnumerable<GetAllBooksDto>>>.Success(paginatedBooks);
    }

    public async Task<Result<BookDetailsDto>> GetBookById(string id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
        {
            return Result<BookDetailsDto>.Fail(new[] { "book not found" });
        }

        var bookDto = _mapper.Map<BookDetailsDto>(book);
        
        return Result<BookDetailsDto>.Success(bookDto);
    }
}