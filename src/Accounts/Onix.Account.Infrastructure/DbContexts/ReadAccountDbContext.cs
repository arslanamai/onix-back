using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Onix.Account.Application.Database;
using Onix.Account.Domain.Accounts;
using Onix.Core.Dtos;

namespace Onix.Account.Infrastructure.DbContexts;

public class ReadAccountDbContext(IConfiguration configuration) : DbContext, IReadAccountDbContext
{
    private const string DATABASE = "Database";
    
    public DbSet<UserDto> Users => Set<UserDto>();
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
            typeof(WriteAccountDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false); 
        modelBuilder.HasDefaultSchema("account");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}