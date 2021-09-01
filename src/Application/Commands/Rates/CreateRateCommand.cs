using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Rates;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Rates
{
    public record CreateRateCommand(CreateRateDto CreateRateDto) : IRequestWrapper<int>{}
    
    public class CreateRateCommandHandler : IHandlerWrapper<CreateRateCommand,int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateRateCommandHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);
        
        public async Task<IResponse<int>> Handle(CreateRateCommand request, CancellationToken cancellationToken)
        {
            var from = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == request.CreateRateDto.From,cancellationToken);
            var to = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == request.CreateRateDto.To,cancellationToken);
            if (await _context.Rates.AnyAsync(x =>
                x.From.Code == from.Code && x.To.Code == to.Code && x.Date == request.CreateRateDto.Date, cancellationToken: cancellationToken))
            {
                return Response.Fail<int>("Already exists such rate.");
            }
            var rate = new Rate(from, to, request.CreateRateDto.Buy, request.CreateRateDto.Sell,
                request.CreateRateDto.Date);
            var entity = (await _context.Rates.AddAsync(rate, cancellationToken)).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            return Response.Success(entity.Id);
        }
    }
}