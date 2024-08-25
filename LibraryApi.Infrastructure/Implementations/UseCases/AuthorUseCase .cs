using AutoMapper;
using LibraryApi.Application.Interfaces.UnitOfWork;
using LibraryApi.Application.Interfaces.UseCases;
using LibraryApi.Application.Models;
using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Application.Validators.Authors;
using LibraryApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Infrastructure.Implementations.UseCases
{
    public class AuthorUseCase
     : IAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AuthorValidator _validator;

        public AuthorUseCase(IUnitOfWork unitOfWork, IMapper mapper, AuthorValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _unitOfWork.Authors.All();
            if (authors != null)
                return new OkObjectResult(_mapper.Map<IEnumerable<AuthorDTO>>(authors));
            return new NotFoundResult();
        }

        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _unitOfWork.Authors.GetById(id);
            if (author != null)
                return new OkObjectResult(_mapper.Map<AuthorDTO>(author));
            return new NotFoundResult();
        }

        public async Task<IActionResult> CreateAuthor(AuthorCreateRequest authorCreateDto)
        {
            if (_validator.ValidateAsync(authorCreateDto).Result.IsValid)
                return new BadRequestResult();

            var author = _mapper.Map<Author>(authorCreateDto);

            var createdAuthor = await _unitOfWork.Authors.Add(author);
            if (createdAuthor)
            {
                await _unitOfWork.CompleteAsync();
                return new OkObjectResult(authorCreateDto);
            }
            return new StatusCodeResult(500);

        }

        public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateResponce authorDto)
        {

            if (id != authorDto.Id || !_validator.ValidateAsync(authorDto).Result.IsValid)
                return new BadRequestResult();

            var author = _mapper.Map<Author>(authorDto);
            var updated = await _unitOfWork.Authors.Update(author);
            if (updated)
            {
                await _unitOfWork.CompleteAsync();
                return new OkObjectResult(authorDto);
            }
            return new NoContentResult();

        }

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _unitOfWork.Authors.Delete(id);

            if (deleted)
            {
                await _unitOfWork.CompleteAsync();
                return new OkResult();
            }

            return new NoContentResult();
        }
    }
}
