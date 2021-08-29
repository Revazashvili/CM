using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Rates;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Rates
{
    public class GetAllRateQuery : IRequestWrapper<IReadOnlyList<RateDto>> { }
    
    public class GetAllRateQueryHandler : IHandlerWrapper<GetAllRateQuery,IReadOnlyList<RateDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllRateQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<IResponse<IReadOnlyList<RateDto>>> Handle(GetAllRateQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<RateDto> rates = await _context.Rates.ProjectToType<RateDto>().ToListAsync(cancellationToken);
            return Response.Success<IReadOnlyList<RateDto>>(rates);
        }
    }
}