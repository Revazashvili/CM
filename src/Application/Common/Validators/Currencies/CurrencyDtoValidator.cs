using Application.Common.DTOs.Currencies;
using FluentValidation;

namespace Application.Common.Validators.Currencies
{
    public class CurrencyDtoValidator : AbstractValidator<CurrencyDto>
    {
        public CurrencyDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().NotNull()
                .Length(3);
            RuleFor(x => x.Name)
                .NotEmpty().NotNull();
            RuleFor(x => x.LatinName)
                .NotEmpty().NotNull();
            RuleFor(x => x.Status)
                .NotEmpty().NotNull()
                .IsInEnum();
        }
    }
}