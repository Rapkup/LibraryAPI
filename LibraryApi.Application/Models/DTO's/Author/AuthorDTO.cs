using LibraryApi.Domain.Models;

namespace LibraryApi.Application.Models
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateOnly Birthday { get; set; }
        public string Country { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
