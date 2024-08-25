using FluentValidation;
using LibraryApi.Application.Models;
using LibraryApi.Application.Models.DTO_s.Book;

namespace LibraryApi.Application.Validators.Books
{
    public class BookValidator : AbstractValidator<CommonFieldsBookDTO>
    {
        public BookValidator()
        {
            RuleFor(book => book.ISBN).NotEmpty().GreaterThan(0);
            RuleFor(book => book.Title).NotEmpty().MaximumLength(255);
            RuleFor(book => book.Genre).MaximumLength(100);
            RuleFor(book => book.Description).MaximumLength(500);
            RuleFor(book => book.AuthorName).NotEmpty().MaximumLength(255);
            RuleFor(book => book.AuthorId).NotEmpty().GreaterThan(0);
            RuleFor(book => book.TakenBy).GreaterThan(0).When(book => book.TakenBy.HasValue);
            RuleFor(book => book.TakenAt).Must(BeAValidDate).When(book => book.TakenAt.HasValue);
            RuleFor(book => book.ShouldBeReturnedAt).Must(BeAValidDate).When(book => book.ShouldBeReturnedAt.HasValue);
        }

        private bool BeAValidDate(DateOnly? date)
        {
            return date.HasValue && date.Value <= DateOnly.FromDateTime(DateTime.Now);
        }

        private bool BeAValidDate(DateTime date)
        {
            return date <= DateTime.Now;
        }
    }
}
