using LibraryApi.Application.Interfaces.UseCases;
using LibraryApi.Application.Models.DTO_s;
using LibraryApi.Application.Models.DTO_s.Requests;
using LibraryApi.Infrastructure.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Server.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly ILoginUseCase _loginUseCase;
        private readonly IRefreshTokenUseCase _refreshTokenUseCase;
        private readonly IRegisterUseCase _registerUseCase;

        public AuthController(ILoginUseCase loginUseCase, IRefreshTokenUseCase refreshTokenUseCase, IRegisterUseCase registerUseCase)
        {
            _loginUseCase = loginUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
            _registerUseCase = registerUseCase;
        }

        [HttpPost("/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _loginUseCase.Execute(userRequest);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest user)
        {
            return await _registerUseCase.Execute(user);
        }

        [HttpPost("RefreshToken")]
        [Authorize(Policy = Policies.AdminOrUser)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            return await _refreshTokenUseCase.Execute(model);
        }
    }
}
