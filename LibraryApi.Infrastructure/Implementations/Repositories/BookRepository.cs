using LibraryApi.Application.Interfaces.Repository;
using LibraryApi.Domain.Models;
using LibraryApi.Infrastructure.Implementations.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Infrastructure.Implementations.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ReposContext context, ILogger logger) : base(context, logger)
        { }

        public override async Task<IEnumerable<Book>> All()
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.Title)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} All function error", typeof(BookRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (result == null)
                    return false;

                result.Status = 0;
                result.UpdatedAt = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Delete function error", typeof(BookRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Book book)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == book.Id);

                if (result == null)
                    return false;

                result.UpdatedAt = DateTime.UtcNow;
                result.ISBN = book.ISBN;
                result.Title = book.Title;
                result.Genre = book.Genre;
                result.Description = book.Description;
                result.AuthorId = book.AuthorId;
                result.TakenBy = book.TakenBy;
                result.TakenAt = book.TakenAt;
                result.ShouldBeReturnedAt = book.ShouldBeReturnedAt;


                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Update function error", typeof(BookRepository));
                throw;
            }
        }


        public virtual async Task<IEnumerable<Book>> GetAllBooksByAuthor(int id)
        {
            try
            {
                return await _dbSet.Where(x => x.AuthorId == id && x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.Title)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetAllBooksByAuthor function error", typeof(BookRepository));
                throw;
            }
        }
        public async Task<IEnumerable<Book>> GetAllBooksByUserLogin(int id)
        {
            try
            {
                return await _dbSet.Where(x => x.TakenBy == id && x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.Title)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetAllBooksByUserLogin function error", typeof(BookRepository));
                throw;
            }
        }
        public async Task<Book?> GetByISBN(int isbn)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.ISBN == isbn && x.Status == 1);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetByISBN function error", typeof(BookRepository));
                throw;
            }
        }
        public Task<bool> GiveBookToUser(int bookId, int UserId)
        {
            throw new NotImplementedException();
        }


    }
}
