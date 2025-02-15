using Microsoft.EntityFrameworkCore;
using Onix.Core.Dtos;

namespace Onix.Account.Application.Database;

public interface IReadAccountDbContext
{
    DbSet<UserDto> Users { get; }
}