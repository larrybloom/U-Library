using Library.API.Responses;
using Library.Core.DTOs;
using Library.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[AllowAnonymous]
[Route("books")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllBooks([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        var result = await _bookService.GetAll(pageSize, pageNumber);

        if (!result.IsSuccessful)
        {
            return BadRequest(ResponseDTO<IEnumerable<string>>.Fail(result.Errors));
        }

        return Ok(ResponseDTO<PaginatorResponseDto<IEnumerable<GetAllBooksDto>>>.Success(result.Data!));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(string id)
    {
        var result = await _bookService.GetBookById(id);
        
        if (!result.IsSuccessful)
        {
            return BadRequest(ResponseDTO<IEnumerable<string>>.Fail(result.Errors));
        }
        
        return Ok(ResponseDTO<BookDetailsDto>.Success(result.Data!));
    }
}