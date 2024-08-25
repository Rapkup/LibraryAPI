using AutoMapper;
using LibraryApi.Application.Interfaces.UnitOfWork;
using LibraryApi.Application.Interfaces.UseCases;
using LibraryApi.Application.Models;
using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Application.Validators.Books;
using LibraryApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Infrastructure.Implementations.UseCases
{
    public class BookUseCase : IBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly BookValidator _validator;

        public BookUseCase(IUnitOfWork unitOfWork, IMapper mapper, BookValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _unitOfWork.Books.All();
            if (books != null)
                return new OkObjectResult(_mapper.Map<IEnumerable<BookDTO>>(books));
            return new NotFoundResult();
        }

        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _unitOfWork.Books.GetById(id);
            if (book != null)
                return new OkObjectResult(_mapper.Map<BookDTO>(book));
            return new NotFoundResult();
        }

        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            var books = await _unitOfWork.Books.GetAllBooksByAuthor(authorId);
            if (books != null)
                return new OkObjectResult(_mapper.Map<IEnumerable<BookDTO>>(books));
            return new NotFoundResult();
        }

        public async Task<IActionResult> GetBooksByUser(int userId)
        {
            var books = await _unitOfWork.Books.GetAllBooksByUserLogin(userId);
            if (books != null)
                return new OkObjectResult(_mapper.Map<IEnumerable<BookDTO>>(books));
            return new NotFoundResult();
        }

        public async Task<IActionResult> GetBookByISBN(int isbn)
        {
            var book = await _unitOfWork.Books.GetByISBN(isbn);
            if (book != null)
                return new OkObjectResult(_mapper.Map<BookDTO>(book));
            return new NotFoundResult();
        }

        public async Task<IActionResult> CreateBook(BookCreateRequest bookDto)
        {
            if (_validator.ValidateAsync(bookDto).Result.IsValid)
                return new BadRequestResult();

            var book = _mapper.Map<Book>(bookDto);
            var created = await _unitOfWork.Books.Add(book);
            if (created)
            {
                await _unitOfWork.CompleteAsync();
                return new OkObjectResult(bookDto);
            }
            return new StatusCodeResult(500);
        }

        public async Task<IActionResult> UpdateBook(int id, BookUpdateResponce bookDto)
        {
            if (_validator.ValidateAsync(bookDto).Result.IsValid)
                return new BadRequestResult();

            if (id != bookDto.Id)
            {
                return new BadRequestResult();
            }

            var book = _mapper.Map<Book>(bookDto);
            var updated = await _unitOfWork.Books.Update(book);
            if (updated)
            {
                await _unitOfWork.CompleteAsync();
                return new OkObjectResult(bookDto);
            }
            return new NoContentResult();
        }


        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _unitOfWork.Books.Delete(id);
            if (deleted)
            {
                await _unitOfWork.CompleteAsync();
                return new OkResult();
            }

            return new NoContentResult();
        }


        public async Task<IActionResult> GiveBookToUser(int bookId, int UserId)
        {
            var result = _unitOfWork.Books.GiveBookToUser(bookId, UserId).Result;

            if (result)
            {
                await _unitOfWork.CompleteAsync();
                return new OkResult();
            }
            return new NoContentResult();
        }
    }
}
