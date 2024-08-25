using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Application.Models.DTO_s;
using LibraryApi.Infrastructure.Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using LibraryApi.Application.Interfaces.Services;
using LibraryApi.Application.Interfaces.UseCases;

namespace LibraryApi.Infrastructure.Implementations.UseCases
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public RefreshTokenUseCase(IConfiguration config, IAuthService authService, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<IActionResult> Execute(RefreshTokenModel model)
        {
            var principal = _authService.GetTokenPrincipal(model.AccessToken);

            var response = new TokenResponse();
            if (principal?.Identity?.Name is null)
                return new UnauthorizedResult();

            var identityUser = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (identityUser is null || identityUser.RefreshToken != model.RefreshToken || identityUser.RefreshTokenExpiryTime < DateTime.Now)
                return new UnauthorizedResult();

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
