namespace LibraryApi.Application.Models.DTO_s.Requests
{
    public class RegisterUserRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
