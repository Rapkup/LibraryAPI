using AutoMapper;
using LibraryApi.Application.Interfaces.UnitOfWork;
using LibraryApi.Application.Models;
using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Domain.Models;
using LibraryApi.Infrastructure.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _unitOfWork.Books.All();
            var bookDtos = _mapper.Map<IEnumerable<BookDTO>>(books);
            return Ok(bookDtos);
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _unitOfWork.Books.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookDto = _mapper.Map<BookDTO>(book);
            return Ok(bookDto);
        }


        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("getByAuthor/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            var books = await _unitOfWork.Books.GetAllBooksByAuthor(authorId);
            var bookDtos = _mapper.Map<IEnumerable<BookDTO>>(books);
            return Ok(bookDtos);
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("getBookByUser/{userId}")]
        public async Task<IActionResult> GetBooksByUser(int userId)
        {
            var books = await _unitOfWork.Books.GetAllBooksByUserLogin(userId);
            var bookDtos = _mapper.Map<IEnumerable<BookDTO>>(books);
            return Ok(bookDtos);
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("getByISBN/{isbn}")]
        public async Task<IActionResult> GetBookByISBN(int isbn)
        {
            var book = await _unitOfWork.Books.GetByISBN(isbn);
            if (book == null)
            {
                return NotFound();
            }
            var bookDto = _mapper.Map<BookDTO>(book);
            return Ok(bookDto);
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateRequest bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = _mapper.Map<Book>(bookDto);
            var created = await _unitOfWork.Books.Add(book);
            if (created)
            {
                await _unitOfWork.CompleteAsync();
                return Ok(bookDto);
            }

            return StatusCode(500, "A problem happened while handling your request.");
        }

        [Authorize(Policy = Policies.User)]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateResponce bookDto)
        {
            if (!ModelState.IsValid || id != bookDto.Id)
            {
                return BadRequest();
            }

            var book = _mapper.Map<Book>(bookDto);
            var updated = await _unitOfWork.Books.Update(book);
            if (updated)
            {
                await _unitOfWork.CompleteAsync();
                return Ok(bookDto);
            }

            return NoContent();
        }

        [Authorize(Policy = Policies.User)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _unitOfWork.Books.Delete(id);
            if (deleted)
            {
                await _unitOfWork.CompleteAsync();
                return Ok(deleted);
            }

            return NoContent();
        }
    }
}
