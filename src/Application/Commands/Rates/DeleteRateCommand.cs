using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;

namespace Application.Commands.Rates
{
    public record DeleteRateCommand(int Id) : IRequestWrapper<bool>{}
    
    public class DeleteRateCommandHandler : IHandlerWrapper<DeleteRateCommand,bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteRateCommandHandler(IApplicationDbContext context) => _context = context;
        
        public async Task<IResponse<bool>> Handle(DeleteRateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rate = await _context.Rates.FindAsync(request.Id);
                if (rate is null) return Response.Fail<bool>("Rate with provided id doesn't exists.");
                _context.Rates.Remove(rate);
                await _context.SaveChangesAsync(cancellationToken);
                return Response.Success(true);
            }
            catch(Exception e)
            {
                return Response.Fail<bool>(new List<string>
                {
                    "Error occured while deleting rate.",
                    e.Message
                });
            }
        }
    }
}