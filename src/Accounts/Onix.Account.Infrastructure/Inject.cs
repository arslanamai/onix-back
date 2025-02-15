using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onix.Account.Application.Database;
using Onix.Account.Infrastructure.DbContexts;
using Onix.Account.Infrastructure.Repositories;
using Onix.Core.Abstraction;
using Onix.Core.DataBase;

namespace Onix.Account.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddAccountInfrastructure(
        this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext()
            .AddDatabase()
            .AddRepositories();
        
        return service;
    }
    
        
    private static IServiceCollection AddRepositories(
        this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepository>();
        
        return service;
    }
    
    private static IServiceCollection AddDbContext(
        this IServiceCollection service)
    {
        service.AddScoped<WriteAccountDbContext>();
        service.AddScoped<IReadAccountDbContext, ReadAccountDbContext>();

        return service;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection service)
    {
        service.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        service.AddScoped<IAccountUnitOfWork, AccountUnitOfWork>();
        
        return service; 
    }
}