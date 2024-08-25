using AutoMapper;
using LibraryApi.Application.Interfaces.UnitOfWork;
using LibraryApi.Application.Interfaces.UseCases;
using LibraryApi.Application.Models;
using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Domain.Models;
using LibraryApi.Infrastructure.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorUseCase _authorUseCase;

        public AuthorController(IAuthorUseCase authorUseCase)
        {
            _authorUseCase = authorUseCase;
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            return await _authorUseCase.GetAllAuthors();

        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            return await _authorUseCase.GetAuthorById(id);

        }

        [Authorize(Policy = Policies.User)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateRequest authorCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _authorUseCase.CreateAuthor(authorCreateDto);

        }

        [Authorize(Policy = Policies.User)]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorUpdateResponce authorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return await _authorUseCase.UpdateAuthor(id, authorDto);
        }

        [Authorize(Policy = Policies.User)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return await _authorUseCase.DeleteAuthor(id);
        }
    }
}
