using Application.Common.DTOs.Rates;
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Common.Validators.Rates
{
    public class CalculateAmountToGiveValidator : AbstractValidator<CalculateAmountToGive>
    {
        public CalculateAmountToGiveValidator(ICurrencyService currencyService)
        {
            RuleFor(x => x.From)
                .NotEmpty().NotNull()
                .MustAsync(currencyService.ExistsAsync);
            RuleFor(x => x.To)
                .NotEmpty().NotNull()
                .MustAsync(currencyService.ExistsAsync);
            RuleFor(x => x.ReceivableAmount)
                .NotNull().GreaterThan(0);
        }
    }
}