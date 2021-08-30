using Application.Common.DTOs.Rates;
using FluentValidation;

namespace Application.Common.Validators.Rates
{
    public class UpdateRateDtoValidator : AbstractValidator<UpdateRateDto>
    {
        public UpdateRateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().GreaterThan(0);
            RuleFor(x => x.Buy)
                .NotNull().GreaterThan(0);
            RuleFor(x => x.Sell)
                .NotNull().GreaterThan(0);
        }
    }
}