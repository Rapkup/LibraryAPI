namespace LibraryApi.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateOnly Birthday { get; set; }
        public string Country { get; set; }

    }
}
