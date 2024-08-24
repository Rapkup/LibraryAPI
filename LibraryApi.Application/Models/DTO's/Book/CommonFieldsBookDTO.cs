namespace LibraryApi.Application.Models.DTO_s.Book
{
    public class CommonFieldsBookDTO
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
    }
}
