using System;
using Application.Common.DTOs.Rates;
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Common.Validators.Rates
{
    public class CreateRateDtoValidator : AbstractValidator<CreateRateDto>
    {
        public CreateRateDtoValidator(ICurrencyService currencyService)
        {
            RuleFor(x => x.From)
                .NotEmpty().NotNull()
                .MustAsync((currencyService.ExistsAsync));
            RuleFor(x => x.To)
                .NotEmpty().NotNull()
                .MustAsync(currencyService.ExistsAsync);
            RuleFor(x => x.Buy)
                .NotNull().GreaterThan(0);
            RuleFor(x => x.Sell)
                .NotNull().GreaterThan(0);
            RuleFor(x => x.Date)
                .NotNull().GreaterThan(DateTime.Now.AddDays(-1));
        }
    }
}