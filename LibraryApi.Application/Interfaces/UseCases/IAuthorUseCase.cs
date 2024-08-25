using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Application.Interfaces.UseCases
{
    public interface IAuthorUseCase
    {
        Task<IActionResult> GetAllAuthors();
        Task<IActionResult> GetAuthorById(int id);
        Task<IActionResult> CreateAuthor(AuthorCreateRequest authorCreateDto);
        Task<IActionResult> UpdateAuthor(int id, AuthorUpdateResponce authorDto);
        Task<IActionResult> DeleteAuthor(int id);
    }
}
