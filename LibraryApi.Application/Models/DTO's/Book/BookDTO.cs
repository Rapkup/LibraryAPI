using LibraryApi.Application.Models.DTO_s.Book;

namespace LibraryApi.Application.Models
{
    public class BookDTO : CommonFieldsBookDTO
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
