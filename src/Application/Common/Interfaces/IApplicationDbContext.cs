using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Convert = Domain.Entities.Convert;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<Rate> Rates { get; set; }
        DbSet<Convert> Converts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}