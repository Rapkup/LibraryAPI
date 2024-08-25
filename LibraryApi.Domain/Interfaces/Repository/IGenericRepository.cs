using System.Linq.Expressions;

namespace LibraryApi.Application.Interfaces.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T?> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
        Task<IQueryable<T>> FindQueryableAsync(Expression<Func<T, bool>> expression, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    }
}
