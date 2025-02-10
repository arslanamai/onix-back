using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Products;

namespace Onix.WebSites.Infrastructure.Configurations.Write;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));

        builder.ComplexProperty(p => p.Name, tb =>
        {
            tb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.NAME_MAX_LENGTH)
                .HasColumnName("name");
        });
        
        builder.ComplexProperty(p => p.Code, tb =>
        {
            tb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.CODE_MAX_LENGTH)
                .HasColumnName("code");
        });

        builder.HasMany(p => p.Photos)
            .WithOne()
            .IsRequired(false)
            .HasForeignKey("product_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}