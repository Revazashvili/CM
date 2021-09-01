using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Rates;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Rates
{
    public class GetAllRateQuery : IRequestWrapper<IReadOnlyList<RateDto>> { }
    
    public class GetAllRateQueryHandler : IHandlerWrapper<GetAllRateQuery,IReadOnlyList<RateDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllRateQueryHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<IResponse<IReadOnlyList<RateDto>>> Handle(GetAllRateQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Rate> rates = await _context.Rates
                .Include(x=>x.From)
                .Include(x=>x.To)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            var rateDtos = _mapper.Map<IReadOnlyList<RateDto>>(rates);
            return Response.Success(rateDtos);
        }
    }
}