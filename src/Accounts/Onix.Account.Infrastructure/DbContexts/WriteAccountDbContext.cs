using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Onix.Account.Domain.Accounts;

namespace Onix.Account.Infrastructure.DbContexts;

public class WriteAccountDbContext(IConfiguration configuration) : DbContext
{
    private const string DATABASE = "Database";
    
    public DbSet<User> Users => Set<User>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly( 
            typeof(WriteAccountDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false); 
        modelBuilder.HasDefaultSchema("account");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}