using LibraryApi.Application.Models.DTO_s.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Application.Interfaces.UseCases
{
    public interface ILoginUseCase
    {
        Task<IActionResult> Execute(UserLoginRequest userRequest);
    }
}
