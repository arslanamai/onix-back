using CSharpFunctionalExtensions;
using Onix.SharedKernel;

namespace Onix.Account.Domain.Accounts.ValueObjects;

public class Auth0Sub
{
    private Auth0Sub(
        string sub)
    {
        Sub = sub;
    }
    
    public string? Sub { get; private set; }

    public static Result<Auth0Sub, Error> Create(string sub)
    {
        return new Auth0Sub(sub);
    }
}