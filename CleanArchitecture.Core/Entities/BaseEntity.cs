using DotNetCore.Domain;

namespace CleanArchitecture.Core.Entities;

public class BaseEntity : Entity<Guid>
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool IsDeleted { get; set; }
}