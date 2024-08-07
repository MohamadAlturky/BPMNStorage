namespace ProjectsManagement.SharedKernel.Contracts.Entities;

public class AccessControlledEntity
{
    public int Id { get; set; }
    public ICollection<AccessControlledEntityRight> AccessControlledEntityRights { get; set; } = [];
}
