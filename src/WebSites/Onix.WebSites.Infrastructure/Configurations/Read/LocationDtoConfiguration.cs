using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onix.Core.Dtos;
using Onix.SharedKernel;

namespace Onix.WebSites.Infrastructure.Configurations.Read;

public class LocationDtoConfiguration : IEntityTypeConfiguration<LocationDto>
{
    public void Configure(EntityTypeBuilder<LocationDto> builder)
    {
        builder.ToTable("location");

        builder.Property(l => l.Id)
            .HasColumnName("Id");
    }
}