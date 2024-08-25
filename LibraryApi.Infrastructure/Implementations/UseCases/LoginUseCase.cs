using LibraryApi.Application.Models.DTO_s.Requests;
using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Infrastructure.Authorization.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using LibraryApi.Application.Interfaces.Services;
using LibraryApi.Application.Interfaces.UseCases;

namespace LibraryApi.Infrastructure.Implementations.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public LoginUseCase(IConfiguration config, UserManager<User> userManager, IAuthService authService)
        {
            _config = config;
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<IActionResult> Execute(UserLoginRequest userRequest)
        {
            var response = new TokenResponse();
            var identityUser = await _userManager.FindByNameAsync(userRequest.Login);

            if (identityUser is null || await _userManager.CheckPasswordAsync(identityUser, userRequest.Password) == false)
            {
                return new UnauthorizedResult();
            }

            response.IsLogedIn = true;
            response.AccessToken = await _authService.GenerateAccessTokenString(identityUser);
            response.RefreshToken = _authService.GenerateRefreshTokenString();

            identityUser.RefreshToken = response.RefreshToken;
            identityUser.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(4);
            await _userManager.UpdateAsync(identityUser);

            return new OkObjectResult(response);
        }
    }
}
