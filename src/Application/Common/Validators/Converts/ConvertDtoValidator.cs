using Application.Common.DTOs.Converts;
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Common.Validators.Converts
{
    public class ConvertDtoValidator : AbstractValidator<ConvertDto>
    {
        public ConvertDtoValidator(ICurrencyService currencyService)
        {
            RuleFor(x => x.From)
                .NotEmpty().NotNull()
                .MustAsync(currencyService.ExistsAsync);
            RuleFor(x => x.To)
                .NotEmpty().NotNull()
                .MustAsync(currencyService.ExistsAsync);
            RuleFor(x => x.Amount)
                .NotNull().GreaterThan(0);
        }
    }
}