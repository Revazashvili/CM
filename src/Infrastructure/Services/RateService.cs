using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    /// <inheritdoc cref="IRateService"/>
    public class RateService : IRateService
    {
        private readonly IApplicationDbContext _context;

        public RateService(IApplicationDbContext context) => _context = context;

        public async Task<bool> ExistsAsync(string code, CancellationToken cancellationToken) =>
            await _context.Rates.AnyAsync(x => x.From.Code == code, cancellationToken);
    }
}