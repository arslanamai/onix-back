using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onix.Core.Dtos;
using Onix.WebSites.Domain.Media;

namespace Onix.WebSites.Infrastructure.Configurations.Read;

public class WebSiteDtoConfiguration : IEntityTypeConfiguration<WebSiteDto>
{
    public void Configure(EntityTypeBuilder<WebSiteDto> builder)
    {
        builder.ToTable("website");

        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.Id)
            .HasColumnName("Id");
        
        builder.Property(w => w.Url)
            .HasColumnName("url");
        
        builder.Property(w => w.SocialMedias)
            .HasColumnName("social_medias")
            .IsRequired(false)
            .HasConversion(
                sm => JsonSerializer.Serialize(sm, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<SocialMediaDto>>(
                    json, JsonSerializerOptions.Default)!);

        builder.Property(w => w.Faqs)
            .HasColumnName("faqs")
            .IsRequired(false)
            .HasConversion(
                faqs => JsonSerializer.Serialize(faqs, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<FaqDto>>(json, JsonSerializerOptions.Default)!);

        builder.Property(w => w.Favicon)
            .HasColumnName("favicon")
            .IsRequired(false)
            .HasConversion(
                favicon => JsonSerializer.Serialize(favicon, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<FaviconDto>>(
                    json, JsonSerializerOptions.Default)!);
         
        builder.HasMany(w => w.Blocks)
            .WithOne()
            .IsRequired(false)
            .HasForeignKey("website_id");
        
        builder.HasMany(w => w.Categories)
            .WithOne()
            .IsRequired(false)
            .HasForeignKey("website_id");
        
        builder.HasMany(c => c.Location)
            .WithOne()
            .IsRequired(false)
            .HasForeignKey("website_id");
    }
}