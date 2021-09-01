using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    /// <inheritdoc cref="ICurrencyService"/>
    public class CurrencyService : ICurrencyService
    {
        private readonly IApplicationDbContext _context;

        public CurrencyService(IApplicationDbContext context) => _context = context;

        public async Task<bool> ExistsAsync(string code, CancellationToken cancellationToken) =>
            await _context.Currencies.AnyAsync(x => x.Code == code, cancellationToken);
    }
}