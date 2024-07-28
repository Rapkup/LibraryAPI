using LibraryApi.Domain.Entities;

namespace LibraryApi.Domain.Models
{
    public class Author : IEntity
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateOnly Birthday { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
