using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onix.Core.Dtos;

namespace Onix.WebSites.Infrastructure.Configurations.Read;

public class ProductDtoConfiguration : IEntityTypeConfiguration<ProductDto>
{
    public void Configure(EntityTypeBuilder<ProductDto> builder)
    {
        builder.ToTable("product");

        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("Id");
        
        builder.HasMany(p => p.Photos)
            .WithOne()
            .HasForeignKey("photo_id")
            .IsRequired(false);
    }
}