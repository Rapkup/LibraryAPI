using LibraryApi.Application.Interfaces.Repository;
using LibraryApi.Domain.Models;
using LibraryApi.Infrastructure.Implementations.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Infrastructure.Implementations.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ReposContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Author>> All()
        {

            return await _dbSet.Where(x => x.Status == 1)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.Name)
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

        public override async Task<bool> Update(Author author)
        {

            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == author.Id);

            if (result == null)
                return false;

            result.UpdatedAt = DateTime.UtcNow;
            result.Name = author.Name;
            result.MiddleName = author.MiddleName;
            result.Birthday = author.Birthday;
            result.Country = author.Country;

            return true;

        }

    }
}
