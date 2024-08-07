using System.Diagnostics;

namespace ProjectsManagement.SharedKernel.Contracts.Entities;

public abstract class EntityBase
{
    public int Id { get; set; }
}

public abstract class AuditableEntityBase
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
