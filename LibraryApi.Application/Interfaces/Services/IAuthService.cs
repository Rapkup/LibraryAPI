using LibraryApi.Application.Models.DTO_s;
using LibraryApi.Application.Models.DTO_s.Requests;
using LibraryApi.Application.Models.DTO_s.Responces;

namespace LibraryApi.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TokenResponse> Login(UserLoginRequest user);
        Task<TokenResponse> RefreshToken(RefreshTokenModel model);
        Task<bool> RegisterUser(UserRegisterRequest user);
    }
}
