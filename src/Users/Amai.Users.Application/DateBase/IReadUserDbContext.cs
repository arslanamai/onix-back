using Amai.Core.Dtos;

namespace Amai.Users.Application.DateBase;

public interface IReadUserDbContext
{
    IQueryable<UserDto> Users { get; }
}