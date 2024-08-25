using LibraryApi.Application.Interfaces.Repository;
using LibraryApi.Domain.Models;
using LibraryApi.Infrastructure.Implementations.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace LibraryApi.Infrastructure.Implementations.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ReposContext context, ILogger logger) : base(context, logger)
        { }

        public override async Task<IEnumerable<Book>> All()
        {

            return await _dbSet.Where(x => x.Status == 1)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.Title)
                .ToListAsync();

        }

        public override async Task<bool> Delete(int id)
        {

            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                return false;

            result.Status = 0;
            result.UpdatedAt = DateTime.UtcNow;

            return true;

        }

        public override async Task<bool> Update(Book book)
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




        public  async Task<IEnumerable<Book>> GetAllBooksByAuthor(int id)
        {

            return await _dbSet.Where(x => x.AuthorId == id && x.Status == 1)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksByUserLogin(int id)
        {

            return await _dbSet.Where(x => x.TakenBy == id && x.Status == 1)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.Title)
                .ToListAsync();
        }


        public async Task<Book?> GetByISBN(int isbn)
        {

            return await _dbSet.FirstOrDefaultAsync(x => x.ISBN == isbn && x.Status == 1);

        }
        public async Task<bool> GiveBookToUser(int bookId, int UserId)
        {
            var book = await _dbSet.FirstAsync(x => x.Id == bookId);

            book.TakenBy = UserId;
            book.TakenAt = DateOnly.FromDateTime(DateTime.Today);
            book.ShouldBeReturnedAt = DateOnly.FromDateTime(DateTime.Today.AddMonths(1));

            var res = await Update(book);

            if (res)
                return true;
            else return false;

        }
    }
}
