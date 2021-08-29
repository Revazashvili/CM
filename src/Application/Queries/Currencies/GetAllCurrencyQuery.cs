using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Currencies;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Currencies
{
    public class GetAllCurrencyQuery : IRequestWrapper<IReadOnlyList<CurrencyDto>> { }
    
    public class GetAllCurrencyQueryHandler : IHandlerWrapper<GetAllCurrencyQuery,IReadOnlyList<CurrencyDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllCurrencyQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<IResponse<IReadOnlyList<CurrencyDto>>> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<CurrencyDto> currencies = await _context.Currencies
                .ProjectToType<CurrencyDto>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return Response.Success(currencies);
        }
    }
}