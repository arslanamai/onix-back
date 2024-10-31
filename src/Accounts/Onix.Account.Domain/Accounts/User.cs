using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Onix.Account.Domain.Accounts.ValueObjects;
using Onix.Account.Domain.Claims;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;

namespace Onix.Account.Domain.Accounts;

public class User : IdentityUser<Guid>
{
    //ef core
    private User()
    {
    }
    
    public IReadOnlyList<Role> Roles => _roles;
    private List<Role> _roles = [];
    
    public static Result<User, Error> Create(
        Name userName, 
        Phone phone,
        Email email,
        PasswordHash passwordHash,
        Role role)
    {
        return new User
        {
            UserName = userName.Value,
            PhoneNumber = phone.Value,
            Email = email.Value,
            PasswordHash = passwordHash.Value,
            _roles = [role]
        };
    }

    public UnitResult<Error> PhoneConfirm()
    {
        this.PhoneNumberConfirmed = true;
        return UnitResult.Success<Error>();
    }
}