using DotNetCore.Domain;

namespace CleanArchitecture.Core.Entities;

public class BaseEntity: Entity<Guid>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
}