namespace ProjectsManagement.SharedKernel.Contracts.Entities;

public class AccessRight
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<AccessControlledEntityRight> AccessControlledEntityRights { get; set; }
}
