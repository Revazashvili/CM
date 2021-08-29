using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class RateConfiguration :  IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.Property(x => x.Sell)
                .IsRequired();
            builder.Property(x => x.Buy)
                .IsRequired();
            builder.Property(x => x.Date)
                .HasDefaultValue(DateTime.Now.Date)
                .IsRequired();
        }
    }
}