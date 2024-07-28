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
            try
            {
                return await _dbSet.Where(x => x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} All function error", typeof(AuthorRepository));
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
                _logger.LogError(e, "{Repo} Delete function error", typeof(AuthorRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Author author)
        {
            try
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
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Update function error", typeof(AuthorRepository));
                throw;
            }
        }

    }
}
