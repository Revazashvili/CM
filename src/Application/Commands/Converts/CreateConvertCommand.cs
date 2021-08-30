using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Converts;
using Application.Common.DTOs.Users;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Convert = Domain.Entities.Convert;

namespace Application.Commands.Converts
{
    public record CreateConvertCommand(ConvertDto ConvertDto) : IRequestWrapper<bool>{}
    
    public class CreateConvertCommandHandler : IHandlerWrapper<CreateConvertCommand,bool>
    {
        private readonly double _maxConvertValue;
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;

        public CreateConvertCommandHandler(IApplicationDbContext context, IUserService userService,
            IConfiguration configuration) =>
            (_context, _userService, _maxConvertValue) = (context, userService,
                configuration.GetSection("MaxConvertAmount").Get<double>());

        public async Task<IResponse<bool>> Handle(CreateConvertCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var convertDto = request.ConvertDto;
                if (convertDto.Amount > _maxConvertValue)
                    return Response.Fail<bool>("Please, Provider your credentials.");
                
                var from = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == convertDto.From,
                    cancellationToken);
                var to = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == convertDto.To, cancellationToken);
                var user = await _userService.FirstOrDefaultAsync(x=>x.Pin == convertDto.ConverterPin, cancellationToken);
                if (user is not null)
                {
                    // არის თუ არა ტიპის, მისი რეკომენდატორების ან ვინც ამ ტიპმა რეკომენდაცია გაუწია დღის მანძილზე დაკონვერტირებული თანხის ექვივალენტი ლარში 100 000-ზე მეტი. 
                }

                user = await _userService.CreateAsync(
                    new CreateUserDto(convertDto.ConverterPin!, convertDto.FirstName!, convertDto.LastName!),
                    cancellationToken);
                var convert = new Convert(from!, to!, user, convertDto.RecommenderPin, convertDto.Amount, DateTime.Now);
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