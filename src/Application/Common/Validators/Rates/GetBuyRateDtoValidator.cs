using Application.Common.DTOs.Rates;
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Common.Validators.Rates
{
    public class GetBuyRateDtoValidator : AbstractValidator<GetBuyRateDto>
    {
        public GetBuyRateDtoValidator(ICurrencyService currencyService)
        {
            RuleFor(x => x.From)
                .NotEmpty().NotNull()
                .MustAsync(currencyService.ExistsAsync);
            RuleFor(x => x.To)
                .NotEmpty().NotNull()
                .MustAsync(currencyService.ExistsAsync);
        }
    }
}