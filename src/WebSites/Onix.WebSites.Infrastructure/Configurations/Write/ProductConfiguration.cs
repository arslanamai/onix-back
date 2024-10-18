using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Categories.Entities;

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
                .HasMaxLength(Constants.NAME_MAX_LENGHT)
                .HasColumnName("name");
        });
        
        builder.ComplexProperty(p => p.Description, tb =>
        {
            tb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.DESCRIPTION_MAX_LENGHT)
                .HasColumnName("description");
        });
        
        builder.ComplexProperty(p => p.Price, tb =>
        {
            tb.Property(n => n.Value)
                .IsRequired(false)
                .HasColumnName("price");
        });
        
        builder.ComplexProperty(p => p.Link, tb =>
        {
            tb.Property(l => l.Value)
                .IsRequired(false)
                .HasMaxLength(Constants.LINK_MAX_LENGHT)
                .HasColumnName("link");
        });

        builder.HasMany(p => p.ProductPhotos)
            .WithOne()
            .IsRequired(false)
            .HasForeignKey("product_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}