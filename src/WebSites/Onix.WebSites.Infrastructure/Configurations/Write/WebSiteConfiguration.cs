using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.WebSites;

namespace Onix.WebSites.Infrastructure.Configurations.Write;

public class WebSiteConfiguration : IEntityTypeConfiguration<WebSite>
{
    public void Configure(EntityTypeBuilder<WebSite> builder)
    {
        builder.ToTable("website");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .HasConversion(
                id => id.Value,
                value => WebSiteId.Create(value));

        builder.ComplexProperty(w => w.Name, tb =>
        {
            tb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.NAME_MAX_LENGTH)
                .HasColumnName("name");
        });
        
        builder.ComplexProperty(w => w.Url, tb =>
        {
            tb.Property(u => u.Value)
                .IsRequired()
                .HasMaxLength(Constants.URL_MAX_LENGTH)
                .HasColumnName("url");
        });
        
        builder.ComplexProperty(w => w.Favicon, tb =>
        {
            tb.Property(f => f.Path)
                .IsRequired()
                .HasMaxLength(Constants.PATH_MAX_LENGTH)
                .HasColumnName("favicon");
        });
        
        builder.Property(w => w.CreatedDate)
            .IsRequired()
            .HasColumnName("created_date");

        builder.Property(w => w.IsPublish)
            .IsRequired()
            .HasColumnName("is_publish");
        
        builder.HasMany(c => c.Locations)
            .WithOne()
            .IsRequired(false)
            .HasForeignKey("website_id")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(w => w.Products)
            .WithOne()
            .HasForeignKey("website_id")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Blocks)
            .WithOne()
            .HasForeignKey("website_id")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}