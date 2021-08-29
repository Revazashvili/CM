using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Converts;
using Application.Common.DTOs.Users;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Microsoft.EntityFrameworkCore;
using Convert = Domain.Entities.Convert;

namespace Application.Commands.Converts
{
    public record CreateConvertCommand(ConvertDto ConvertDto) : IRequestWrapper<bool>{}
    
    public class CreateConvertCommandHandler : IHandlerWrapper<CreateConvertCommand,bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;

        public CreateConvertCommandHandler(IApplicationDbContext context, IUserService userService) =>
            (_context, _userService) = (context, userService);

        public async Task<IResponse<bool>> Handle(CreateConvertCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var convertDto = request.ConvertDto;
                var from = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == convertDto.From,
                    cancellationToken);
                var to = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == convertDto.To, cancellationToken);
                var user = await _userService.GetByPin(convertDto.ConverterPin!, cancellationToken);
                if (user is null)
                    user = await _userService.Create(
                        new CreateUserDto(convertDto.ConverterPin, convertDto.FirstName!, convertDto.LastName!),
                        cancellationToken);
                var convert = new Convert(from!, to!, user!, convertDto.RecommenderPin!, convertDto.Amount, DateTime.Now);
                await _context.Converts.AddAsync(convert, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return Response.Success(true);
            }
            catch (Exception e)
            {
                return Response.Fail<bool>(e.Message);
            }
        }
    }
}