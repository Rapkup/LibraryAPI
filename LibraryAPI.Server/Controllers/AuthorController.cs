using AutoMapper;
using LibraryApi.Application.Interfaces.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _unitOfWork.Authors.All();
            var authorDtos = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
            return Ok(authorDtos);
        }

        [Authorize(Policy = Policies.AdminOrUser)]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _unitOfWork.Authors.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            var authorDto = _mapper.Map<AuthorDTO>(author);
            return Ok(authorDto);
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateResponse authorCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var author = _mapper.Map<Author>(authorCreateDto);
            var createdAuthor = await _unitOfWork.Authors.Add(author);
            if (createdAuthor)
            {
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [Authorize(Policy = Policies.User)]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorUpdateResponce authorDto)
        {
            if (id != authorDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var author = _mapper.Map<Author>(authorDto);
            var updated = await _unitOfWork.Authors.Update(author);
            await _unitOfWork.CompleteAsync();

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Policy = Policies.User)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _unitOfWork.Authors.Delete(id);
            await _unitOfWork.CompleteAsync();

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
