using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Currencies;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Currencies
{
    public record CreateCurrencyCommand(CurrencyDto CurrencyDto) : IRequestWrapper<string?>{}
    
    public class CreateCurrencyCommandHandler : IHandlerWrapper<CreateCurrencyCommand,string?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCurrencyCommandHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<IResponse<string?>> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = _mapper.Map<Currency>(request.CurrencyDto);
            await _context.Currencies.AddAsync(currency, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Response.Success(currency.Code);
        }
    }
}