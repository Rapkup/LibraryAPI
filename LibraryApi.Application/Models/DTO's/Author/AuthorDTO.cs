using LibraryApi.Application.Models.DTO_s.Author;

namespace LibraryApi.Application.Models
{
    public class AuthorDTO : CommonFieldsAuthorDTO
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
