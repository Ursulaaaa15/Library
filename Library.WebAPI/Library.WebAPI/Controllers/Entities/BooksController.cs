using AutoMapper;
using Library.BL.Books;
using Library.BL.Books.Entities;
using Library.WebAPI.Controllers.Entities.Books;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBooksProvider _booksProvider;
    private readonly IBooksManager _booksManager;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public BooksController(IBooksProvider booksProvider, IBooksManager booksManager, IMapper mapper, ILogger<BooksController> logger) 
    {
        _booksManager = booksManager;
        _booksProvider = booksProvider;
        _mapper = mapper;
        _logger = logger;  
    }
    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var books = _booksProvider.GetBooks();
        return Ok(new BooksListResponse()
        {
            Books = books.ToList()
        });
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilterBook([FromQuery] FilterBook filter)
    {
        var books = _booksProvider.GetBooks(_mapper.Map<BooksFilter>(filter));
        return Ok(new BooksListResponse()
        {
            Books = books.ToList()
        });
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetBookInfo([FromRoute]Guid id) 
    {
        try
        {
            var book = _booksProvider.GetBookInfo(id);
            return Ok(book);
        }
        catch (ArgumentException ex) 
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateBook([FromBody]CreateBookRequest request)
    {
        var book = _booksManager.CreateBook(_mapper.Map<CreateBookModel>(request));
        return Ok(book);
    }

    [HttpPost]
    [Route("{id}")]
    public IActionResult UpdateBookInfo([FromRoute] Guid id, UpdateBookRequest request)
    {
        try
        {
            BookModel book = _booksManager.UpdateBook(id, _mapper.Map<UpdateBookModel>(request));
            return Ok(book);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteBook([FromRoute] Guid id)
    {
        try
        {
            _booksManager.DeleteBook(id);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest(ex.Message);
        }
    }
}

