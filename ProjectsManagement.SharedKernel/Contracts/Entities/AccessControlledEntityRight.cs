namespace ProjectsManagement.SharedKernel.Contracts.Entities;

public class AccessControlledEntityRight
{
    public int Id { get; set; }
    public int AccessRight { get; set; }
    public int AccessControlledEntity { get; set; }
    public AccessControlledEntity AccessControlledEntityNavigation { get; set; } = null!;
    public AccessRight AccessRightNavigation { get; set; } = null!;
}