using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ConvertConfiguration : IEntityTypeConfiguration<Convert>
    {
        public void Configure(EntityTypeBuilder<Convert> builder)
        {
            builder.Property(x => x.RecommenderPin)
                .IsRequired(false)
                .HasMaxLength(11)
                .IsFixedLength();
            builder.HasOne(x => x.From)
                .WithMany()
                .IsRequired();
            builder.HasOne(x => x.To)
                .WithMany()
                .IsRequired();
            builder.HasOne(x=>x.Converter)
                .WithMany()
                .IsRequired();
        }
    }
}