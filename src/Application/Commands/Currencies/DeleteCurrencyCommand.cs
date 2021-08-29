using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Currencies
{
    public record DeleteCurrencyCommand(string Code) : IRequestWrapper<bool>{}
    
    public class DeleteCurrencyCommandHandler : IHandlerWrapper<DeleteCurrencyCommand,bool>
    {
        private readonly IApplicationDbContext _context;
        
        public DeleteCurrencyCommandHandler(IApplicationDbContext context) => _context = context;

        public  async Task<IResponse<bool>> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currency =
                    await _context.Currencies.FirstOrDefaultAsync(x => x.Code == request.Code, cancellationToken);
                if (currency is null) return Response.Fail<bool>("Currency with provided id doesn't exists.");
                _context.Currencies.Remove(currency);
                await _context.SaveChangesAsync(cancellationToken);
                return Response.Success<bool>(true);
            }
            catch (Exception e)
            {
                return Response.Fail<bool>(new List<string>
                {
                    "Error occured while deleting currency.",
                    e.Message
                });
            }
        }
    }
}