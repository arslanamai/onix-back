using Onix.Account.Domain.Accounts.ValueObjects;
using Onix.SharedKernel.ValueObjects;

namespace Onix.Account.Domain.Accounts;

public class Account
{
    //ef core
    private Account()
    {
        
    }
    
    public Account(
        Email email,
        PasswordHash passwordHash,
        Name name,
        Phone phone)
    {
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
        Phone = phone;
    }

    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public Name Name { get; private set; }
    public Phone Phone { get; private set; }
}