using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Onix.Core.Dtos;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Infrastructure.DbContexts;

public class WebSiteReadDbContext(IConfiguration configuration) : DbContext, IWebSiteReadDbContext
{
    private const string DATABASE = "Database";
    
    public IQueryable<WebSiteDto> WebSites => Set<WebSiteDto>();
    public IQueryable<LocationDto> Location => Set<LocationDto>();
    public IQueryable<BlockDto> Blocks => Set<BlockDto>();
    public IQueryable<ProductDto> Product => Set<ProductDto>();
    public IQueryable<PhotoDto> Photo => Set<PhotoDto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WebSiteReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
        modelBuilder.HasDefaultSchema("website");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}