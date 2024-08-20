namespace LibraryApi.Application.Models.DTO_s.Responces
{
    public class AuthorCreateResponse
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateOnly Birthday { get; set; }
        public string Country { get; set; }
    }
}
