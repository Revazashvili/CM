using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Currencies;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Currencies
{
    public record UpdateCurrencyCommand(CurrencyDto CurrencyDto) : IRequestWrapper<string?>{}

    public class UpdateCurrencyCommandHandler : IHandlerWrapper<UpdateCurrencyCommand,string?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCurrencyCommandHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<IResponse<string?>> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currencyToUpdate =
                await _context.Currencies
                    .FirstOrDefaultAsync(x => x.Code == request.CurrencyDto.Code, cancellationToken);
            if (currencyToUpdate is null) 
                return Response.Fail<string?>("Currency with provided id doesn't exists.");
            _mapper.Map(request.CurrencyDto, currencyToUpdate);
            await _context.SaveChangesAsync(cancellationToken);
            return Response.Success(currencyToUpdate.Code);
        }
    }
}