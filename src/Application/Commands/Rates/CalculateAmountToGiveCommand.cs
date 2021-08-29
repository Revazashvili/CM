using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Rates;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;

namespace Application.Commands.Rates
{
    public record CalculateAmountToGiveCommand(CalculateAmountToGive AmountToGive) : IRequestWrapper<double>{}
    
    public class CalculateAmountToGiveCommandHandler : IHandlerWrapper<CalculateAmountToGiveCommand,double>
    {
        private readonly ICurrencyCalculator _currencyCalculator;

        public CalculateAmountToGiveCommandHandler(ICurrencyCalculator currencyCalculator) =>
            _currencyCalculator = currencyCalculator;

        public async Task<IResponse<double>> Handle(CalculateAmountToGiveCommand request, CancellationToken cancellationToken)
        {
            var amountToGiveDto = request.AmountToGive;
            var amount = await _currencyCalculator.AmountForGiving(amountToGiveDto.From, amountToGiveDto.To,
                amountToGiveDto.ReceivableAmount);
            return Response.Success(amount);
        }
    }
}