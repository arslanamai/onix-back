using Microsoft.AspNetCore.Identity;

namespace Onix.Account.Domain.Claims;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermissions { get; set; } = [];
}