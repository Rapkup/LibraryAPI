using LibraryApi.Application.Models.DTO_s.Responces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Application.Interfaces.UseCases
{
    public interface IBookUseCase
    {
        Task<IActionResult> GetAllBooks();
        Task<IActionResult> GetBookById(int id);
        Task<IActionResult> GetBooksByAuthor(int authorId);
        Task<IActionResult> GetBooksByUser(int userId);
        Task<IActionResult> GetBookByISBN(int isbn);
        Task<IActionResult> CreateBook(BookCreateRequest bookDto);
        Task<IActionResult> UpdateBook(int id, BookUpdateResponce bookDto);
        Task<IActionResult> DeleteBook(int id);
        Task<IActionResult> GiveBookToUser(int bookId, int UserId);
    }
}
