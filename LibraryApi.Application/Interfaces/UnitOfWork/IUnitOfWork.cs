using LibraryApi.Application.Interfaces.Repository;

namespace LibraryApi.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }

        Task<bool> CompleteAsync();
    }
}
