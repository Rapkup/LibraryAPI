using LibraryApi.Application.Interfaces.UseCases;
using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Infrastructure.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookUseCase _bookUseCase;

        public BookController(IBookUseCase bookUseCase)
        {
            _bookUseCase = bookUseCase;
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            return await _bookUseCase.GetAllBooks();
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            return await _bookUseCase.GetBookById(id);
        }


        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("getByAuthor/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            return await _bookUseCase.GetBooksByAuthor(authorId);
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("getBookByUser/{userId}")]
        public async Task<IActionResult> GetBooksByUser(int userId)
        {
            return await _bookUseCase.GetBooksByUser(userId);
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("getByISBN/{isbn}")]
        public async Task<IActionResult> GetBookByISBN(int isbn)
        {
            return await _bookUseCase.GetBookByISBN(isbn);
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateRequest bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _bookUseCase.CreateBook(bookDto);
        }

        [Authorize(Policy = Policies.User)]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateResponce bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return await _bookUseCase.UpdateBook(id, bookDto);
        }

        [Authorize(Policy = Policies.User)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return await _bookUseCase.DeleteBook(id);
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost("borrow-book/{id}")]
        public async Task<IActionResult> GiveBookToUser(int bookId, int userId)
        {
            return await _bookUseCase.GiveBookToUser(bookId, userId);
        }

    }
}
