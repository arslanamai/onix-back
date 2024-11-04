using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Locations;
using Onix.WebSites.Domain.Locations.ValueObjects;

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
        
        builder.ComplexProperty(l => l.LocationPhone, tb =>
        {
            tb.Property(p => p.Value)
                .IsRequired(false)
                .HasMaxLength(Constants.PHONE_MAX_LENGTH)
                .HasColumnName("phone");
        });
        
        builder.ComplexProperty(l => l.LocationAddress, tb =>
        {
            tb.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(Constants.ADDRESS_MAX_LENGTH)
                .HasColumnName("city");
            
            tb.Property(p => p.Street)
                .IsRequired()
                .HasMaxLength(Constants.ADDRESS_MAX_LENGTH)
                .HasColumnName("street");
            
            tb.Property(p => p.Build)
                .IsRequired()
                .HasMaxLength(Constants.ADDRESS_MAX_LENGTH)
                .HasColumnName("build");
            
            tb.Property(p => p.Index)
                .IsRequired(false)
                .HasMaxLength(Constants.INDEX_MAX_LENGHT)
                .HasColumnName("index");
        });
        
        builder.Property(w => w.Schedules)
            .HasColumnName("schedules")
            .HasMaxLength(Constants.JSON_MAX_LENGTH)
            .IsRequired(false)
            .HasConversion(
                sc => JsonSerializer.Serialize(sc, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<Schedule>>(json, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<Schedule>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));;
    }
}