using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Infrastructure.Authorization.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
