using LibraryApi.Application.Interfaces.Services;
using LibraryApi.Application.Interfaces.UseCases;
using LibraryApi.Application.Models.DTO_s.Requests;
using LibraryApi.Infrastructure.Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LibraryApi.Infrastructure.Implementations.UseCases
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public RegisterUseCase(IConfiguration config, IAuthService authService, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<IActionResult> Execute(UserRegisterRequest user)
        {
            var userExists = await _userManager.FindByNameAsync(user.UserName);
            if (userExists != null)
            {
                return new BadRequestObjectResult("User with this nickname already exists");
            }

            User newUser = new User()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
                return new BadRequestObjectResult("Something went wrong");

            await _userManager.AddToRoleAsync(newUser, "User");

            var loginResult = await new LoginUseCase(_config, _userManager, _authService).Execute(new UserLoginRequest { Login = newUser.UserName, Password = user.Password });

            if (loginResult is OkObjectResult)
                return new OkObjectResult("Successfully done\n" + loginResult);

            return new BadRequestObjectResult("Something went wrong");
        }
    }
}
