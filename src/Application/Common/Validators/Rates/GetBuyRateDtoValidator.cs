using Application.Common.DTOs.Rates;
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Common.Validators.Rates
{
    public class GetBuyRateDtoValidator : AbstractValidator<GetBuyRateDto>
    {
        public GetBuyRateDtoValidator(IRateService rateService)
        {
            RuleFor(x => x.From)
                .NotEmpty().NotNull()
                .MustAsync(rateService.ExistsAsync);
            RuleFor(x => x.To)
                .NotEmpty().NotNull()
                .MustAsync(rateService.ExistsAsync);
        }
    }
}