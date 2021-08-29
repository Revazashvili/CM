using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class CurrencyConfiguration :  IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Code);
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.Code)
                .HasMaxLength(3)
                .IsFixedLength();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.LatinName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Status)
                .IsRequired();
        }
    }
}