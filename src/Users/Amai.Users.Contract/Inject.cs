using Amai.Users.Contract.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Onix.Core.Contracts.Users;

namespace Amai.Users.Contract;

public static class Inject
{
    public static IServiceCollection AddUserContract(
        this IServiceCollection services)
    {
        services.AddContract();
        
        return services;
    }
    
    private static IServiceCollection AddContract(
        this IServiceCollection services)
    {
        services.AddScoped<IUserContract, UserContract>();
        
        return services;
    }
    
}