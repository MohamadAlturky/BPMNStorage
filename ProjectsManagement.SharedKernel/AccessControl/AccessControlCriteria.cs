namespace ProjectsManagement.SharedKernel.AccessControl;

public class AccessControlCriteria
{
    public int Id { get; set; }
    public int? ContributionType { get; set; }
    public AccessedRecourceType AccessedRecourceType { get; set; }
}


public enum AccessedRecourceType
{
    PROJECT,
    INVITATION,
    ACTIVITY,
    CONTRIBUTION_MEMBER,
    TASK
}