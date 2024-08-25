using LibraryApi.Domain.Models;

namespace LibraryApi.Application.Interfaces.Repository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book?> GetByISBN(int id);
        Task<IEnumerable<Book>> GetAllBooksByUserLogin(int id);
        Task<IEnumerable<Book>> GetAllBooksByAuthor(int id);
        Task<bool> GiveBookToUser(int bookId, int UserId);
    }
}
