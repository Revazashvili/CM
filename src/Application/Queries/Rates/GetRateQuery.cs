using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Rates;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Rates
{
    public record GetRateQuery(Expression<Func<Rate,bool>> Predicate) : IRequestWrapper<RateDto>{}
    
    public class GetRateQueryHandler : IHandlerWrapper<GetRateQuery,RateDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetRateQueryHandler(IApplicationDbContext context,IMapper mapper) => (_context,_mapper) = (context,mapper);
        
        public async Task<IResponse<RateDto>> Handle(GetRateQuery request, CancellationToken cancellationToken)
        {
            var rate = await _context.Rates
                .Include(x=>x.From)
                .Include(x=>x.To)
                .AsNoTracking()
                .FirstOrDefaultAsync(request.Predicate, cancellationToken);
            return Response.Success(_mapper.Map<RateDto>(rate));
        }
    }
}