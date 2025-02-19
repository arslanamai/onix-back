using System.ComponentModel.DataAnnotations.Schema;

namespace Onix.Core.Dtos;

public class BlockDto
{
     public Guid Id { get; init; }
     //public Guid WebSiteId { get; init; }
     public string Code { get; init; } = string.Empty;
     public int Index { get; init; }
}