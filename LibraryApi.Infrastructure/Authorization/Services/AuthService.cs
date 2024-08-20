using LibraryApi.Application.Interfaces.Services;
using LibraryApi.Application.Models.DTO_s;
using LibraryApi.Application.Models.DTO_s.Requests;
using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Infrastructure.Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public AuthService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<TokenResponse> Login(LoginUserRequest user)
        {
            var response = new TokenResponse();
            var identityUser = await _userManager.FindByNameAsync(user.Login);

            if (identityUser is null || (await _userManager.CheckPasswordAsync(identityUser, user.Password)) == false)
            {
                return response;
            }

            response.IsLogedIn = true;
            response.AccessToken = this.GenerateAccessTokenString(identityUser).Result;
            response.RefreshToken = this.GenerateRefreshTokenString();

            identityUser.RefreshToken = response.RefreshToken;

            identityUser.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(4);
            await _userManager.UpdateAsync(identityUser);

            return response;
        }

        public async Task<bool> RegisterUser(RegisterUserRequest user)
        {
            var userExists = await _userManager.FindByNameAsync(user.UserName);
            if (userExists != null)
            {
                new Exception("User with this nickname already exists");
                return false;
            }

            User newUser = new User()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded != true)
                return false;

            await _userManager.AddToRoleAsync(newUser, "User");

            var loginResult = await Login(new LoginUserRequest { Login = newUser.UserName, Password = user.Password });

            if(loginResult.IsLogedIn != true)
                return false;

            return true;
        }

        public async Task<TokenResponse> RefreshToken(RefreshTokenModel model)
        {
            var principal = GetTokenPrincipal(model.AccessToken);

            var response = new TokenResponse();
            if (principal?.Identity?.Name is null)
                return response;

            var identityUser = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (identityUser is null || identityUser.RefreshToken != model.RefreshToken || identityUser.RefreshTokenExpiryTime < DateTime.Now)
                return response;

            response.IsLogedIn = true;
            response.AccessToken = this.GenerateAccessTokenString(identityUser).Result;
            response.RefreshToken = this.GenerateRefreshTokenString();

            identityUser.RefreshToken = response.RefreshToken;
            identityUser.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(4);
            await _userManager.UpdateAsync(identityUser);

            return response;
        }

        private async Task<string> GenerateAccessTokenString(User user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var siningCred = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(4),
                claims: authClaims,
                signingCredentials: siningCred
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[64];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetTokenPrincipal(string token)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Secret").Value));

            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

    }
}
