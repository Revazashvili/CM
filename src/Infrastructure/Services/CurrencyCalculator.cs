using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    /// <inheritdoc cref="ICurrencyCalculator"/>
    public class CurrencyCalculator : ICurrencyCalculator
    {
        private readonly IApplicationDbContext _context;

        public CurrencyCalculator(IApplicationDbContext context) => _context = context;

        public async ValueTask<double> AmountForGiving(string from, string to, double receiveAmount)
        {
            var rate = await _context.Rates
                .Include(x => x.From)
                .Include(x => x.To)
                .FirstOrDefaultAsync(x => x.From.Code == from &&
                                          x.To.Code == to &&
                                          x.Date.Day == DateTime.Now.Day);
            return receiveAmount / rate.Buy;
        }
    }
}