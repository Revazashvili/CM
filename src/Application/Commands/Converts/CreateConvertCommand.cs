using System;
using System.Linq;
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
        private readonly double _hierarchyConvertLimit;
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        public CreateConvertCommandHandler(IApplicationDbContext context, IUserService userService,
            IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _maxConvertValue = configuration.GetSection("MaxConvertAmount").Get<double>();
            _hierarchyConvertLimit = configuration.GetSection("HierarchyConvertLimit").Get<double>();
        }

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
                    var totalConvertedMoneyAmount = await CalculateFullHierarchyConvertedMoney(user.Pin);
                    if (totalConvertedMoneyAmount > _hierarchyConvertLimit)
                        return Response.Fail<bool>("Error occured while converting.");
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

        private async ValueTask<double?> CalculateFullHierarchyConvertedMoney(string userPin)
        {
            var converts = await _context.Converts
                .Include(x=>x.From)
                .AsNoTracking()
                .Where(x => (x.Converter.Pin.Equals(userPin) || x.RecommenderPin.Equals(userPin)) && x.Time.Date == DateTime.Now.Date)
                .ToListAsync();
            if (converts is null && !converts!.Any())
                return null;

            var totalAmount = 0.0;
            foreach (var convert in converts)
            {
                var rate = await _context.Rates
                    .FirstOrDefaultAsync(x =>
                        x.From.Code == convert.From.Code && x.To.Code == "GEL" && x.Date.Date == DateTime.Now.Date);
                if (rate is not null)
                {
                    totalAmount += convert.Amount * rate.Buy;
                }
            }

            return totalAmount;
        }
    }
}