using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Rates;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using MapsterMapper;

namespace Application.Commands.Rates
{
    public record UpdateRateCommand(UpdateRateDto RateDto) : IRequestWrapper<int>{}
    
    public class UpdateRateCommandHandler : IHandlerWrapper<UpdateRateCommand,int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateRateCommandHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);
        
        public async Task<IResponse<int>> Handle(UpdateRateCommand request, CancellationToken cancellationToken)
        {
            var rate = await _context.Rates.FindAsync(request.RateDto.Id);
            if(rate is null) return Response.Fail<int>("Can't find rate with provided id.");
            _mapper.Map(request.RateDto, rate);
            await _context.SaveChangesAsync(cancellationToken);
            return Response.Success(rate.Id);
        }
    }
}