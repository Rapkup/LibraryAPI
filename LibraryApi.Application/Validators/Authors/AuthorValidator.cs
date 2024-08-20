using FluentValidation;
using LibraryApi.Application.Models;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Models;

namespace LibraryApi.Application.Validators.Authors
{
    public class AuthorValidator : AbstractValidator<AuthorDTO>
    {
        public AuthorValidator()
        {
            RuleFor(author => author.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(author => author.MiddleName)
                .MaximumLength(100).WithMessage("MiddleName must not exceed 100 characters.");

            RuleFor(author => author.Birthday)
                .NotEmpty().WithMessage("Birthday is required.")
                .Must(BeAValidDate).WithMessage("Invalid birthday date.");

            RuleFor(author => author.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");
        }

        private bool BeAValidDate(DateOnly date)
        {
            return date <= DateOnly.FromDateTime(DateTime.Now);
        }
    }
}