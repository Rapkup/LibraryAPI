using LibraryApi.Application.Interfaces.Repository;
using LibraryApi.Application.Interfaces.UnitOfWork;
using LibraryApi.Infrastructure.Implementations.Contexts;
using LibraryApi.Infrastructure.Implementations.Repositories;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Infrastructure.Implementations.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ReposContext _context;

        public IBookRepository Books { get; }
        public IAuthorRepository Authors { get; }

        public UnitOfWork(ReposContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            Books = new BookRepository(_context, logger);
            Authors = new AuthorRepository(_context, logger);
        }

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
