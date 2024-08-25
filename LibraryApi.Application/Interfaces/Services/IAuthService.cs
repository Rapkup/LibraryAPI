using LibraryApi.Infrastructure.Authorization.Models;
using System.Security.Claims;

namespace LibraryApi.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> GenerateAccessTokenString(User user);
        string GenerateRefreshTokenString();
        ClaimsPrincipal? GetTokenPrincipal(string token);

    }
}
