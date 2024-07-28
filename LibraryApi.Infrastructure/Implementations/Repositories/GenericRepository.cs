using LibraryApi.Application.Interfaces.Repository;
using LibraryApi.Domain.Entities;
using LibraryApi.Infrastructure.Implementations.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;

namespace LibraryApi.Infrastructure.Implementations.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly ILogger _logger;
        protected ReposContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(ReposContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;

            _dbSet = context.Set<T>();
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<T>> FindQueryableAsync(Expression<Func<T, bool>> expression, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = _dbSet.Where(expression);
            return await Task.FromResult(orderBy != null ? orderBy(query) : query);
        }


    }
}
