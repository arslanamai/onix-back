using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onix.Account.Infrastructure.DbContexts;
using Onix.Core.Abstraction;
using Onix.Core.SqlConnect;

namespace Onix.Account.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddAccountInfrastructure(
        this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext()
            .AddDatabase();
        
        return service;
    }
    
    private static IServiceCollection AddDbContext(
        this IServiceCollection service)
    {
        service.AddScoped<AccountDbContext>();

        return service;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection service)
    {
        service.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        
        return service; 
    }
}