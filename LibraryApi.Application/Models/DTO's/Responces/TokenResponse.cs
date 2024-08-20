namespace LibraryApi.Application.Models.DTO_s.Responces
{
    public class TokenResponse
    {
        public bool IsLogedIn { get; set; } = false;
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
