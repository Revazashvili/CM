using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Reports;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Reports
{
    public record GetPersonConvertNumbersQuery(DateTime? From,DateTime? To) : IRequestWrapper<IReadOnlyList<PersonConvertNumbersDto>>{}
    
    public class GetPersonConvertNumbersQueryHandler : IHandlerWrapper<GetPersonConvertNumbersQuery,IReadOnlyList<PersonConvertNumbersDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetPersonConvertNumbersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResponse<IReadOnlyList<PersonConvertNumbersDto>>> Handle(GetPersonConvertNumbersQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<PersonConvertNumbersDto> result = await _context.Converts
                .Include(x => x.Converter)
                .Where(x => x.Converter != null!)
                .GroupBy(x => x.Converter!.Pin)
                .Select(x => new PersonConvertNumbersDto
                {
                    Pin = x.Key,
                    OwnConverts = x.Count(),
                    RelatedPeopleConverts = _context.Converts.Count(g => g.RecommenderPin == x.Key)
                }).ToListAsync(cancellationToken);
            return Response.Success<IReadOnlyList<PersonConvertNumbersDto>>(result);
        }
    }
}