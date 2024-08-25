using LibraryApi.Application.Models.DTO_s.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Application.Interfaces.UseCases
{
    public interface IRegisterUseCase
    {
        Task<IActionResult> Execute(UserRegisterRequest user);
    }
}
