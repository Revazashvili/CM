using Application.Common.DTOs.Converts;
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Common.Validators.Converts
{
    public class ConvertDtoValidator : AbstractValidator<ConvertDto>
    {
        public ConvertDtoValidator(IRateService rateService)
        {
            RuleFor(x => x.From)
                .NotEmpty().NotNull()
                .MustAsync(rateService.ExistsAsync);
            RuleFor(x => x.To)
                .NotEmpty().NotNull()
                .MustAsync(rateService.ExistsAsync);
            RuleFor(x => x.Amount)
                .NotNull().GreaterThan(0);
        }
    }
}