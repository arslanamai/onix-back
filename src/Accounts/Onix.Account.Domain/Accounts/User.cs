using CSharpFunctionalExtensions;
using Onix.Account.Domain.Accounts.ValueObjects;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;

namespace Onix.Account.Domain.Accounts;

public class User : SharedKernel.Entity<UserId>
{
    //ef core
    private User(UserId id) : base(id)
    {
    }
    private User(
        UserId id,
        Email email,
        DateTime registrationDate,
        Auth0Sub auth0Sub) : base(id)
    {
        Email = email;
        RegistrationDate = registrationDate;
        Auth0Sub = auth0Sub;
    }
    
    public Email Email { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public Auth0Sub Auth0Sub { get; private set; }

    public static Result<User, Error> Create(
        UserId id,
        Email email,
        DateTime registrationDate,
        Auth0Sub auth0Sub)
    {
        return new User(
            id,
            email,
            registrationDate,
            auth0Sub);
    }
}