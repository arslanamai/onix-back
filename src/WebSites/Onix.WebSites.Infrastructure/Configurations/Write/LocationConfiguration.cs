using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Locations;

namespace Onix.WebSites.Infrastructure.Configurations.Write;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("location");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .HasConversion(
                id => id.Value,
                value => LocationId.Create(value));

        builder.ComplexProperty(l => l.Name, tb =>
        {
            tb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.NAME_MAX_LENGTH)
                .HasColumnName("name");
        });

        builder.ComplexProperty(l => l.Code, tb =>
        {
            tb.Property(c => c.Value)
                .IsRequired(false)
                .HasMaxLength(Constants.CODE_MAX_LENGTH)
                .HasColumnName("code");
        });
    }
}