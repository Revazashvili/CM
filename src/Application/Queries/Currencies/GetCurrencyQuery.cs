using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Currencies;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Currencies
{
    public record GetCurrencyQuery(Expression<Func<Currency,bool>> Predicate) : IRequestWrapper<CurrencyDto>{}
    
    public class GetCurrencyQueryHandler : IHandlerWrapper<GetCurrencyQuery,CurrencyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCurrencyQueryHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<IResponse<CurrencyDto>> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
        {
            var currency = await _context.Currencies.FirstOrDefaultAsync(request.Predicate, cancellationToken);
            return Response.Success(_mapper.Map<CurrencyDto>(currency));
        }
    }
}