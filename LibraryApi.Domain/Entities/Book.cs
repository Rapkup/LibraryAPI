using LibraryApi.Domain.Entities;

namespace LibraryApi.Domain.Models
{
    public class Book: IEntity
    {
        public int ISBN { get; set; } 
        public string Title { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }

        public string AuthorName { get; set; }
        public int AuthorId { get; set; }

        public int? TakenBy { get; set; }
        public DateOnly? TakenAt { get; set; }
        public DateOnly? ShouldBeReturnedAt { get; set; }

        public virtual Author? Author { get; set; }
    }
}
