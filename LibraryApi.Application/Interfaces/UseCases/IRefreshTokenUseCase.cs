using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Application.Models.DTO_s;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Application.Interfaces.UseCases
{
    public interface IRefreshTokenUseCase
    {
        Task<IActionResult> Execute(RefreshTokenModel model);
    }
}
