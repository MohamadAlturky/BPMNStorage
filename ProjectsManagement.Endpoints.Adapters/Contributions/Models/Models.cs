using ProjectsManagement.Core.Contributions;

namespace ProjectsManagement.API.Endpoints.ContributionMembers;

public class GetContributionMemberByIdRequest
{
    public int Id { get; set; }
}

public class FilterContributionMemberRequest
{
    public ContributionMemberFilter Filter { get; set; }
}

public class UpdateContributionMemberRequest
{
    public int Id { get; set; }
    public int Project { get; set; }
    public int Contributor { get; set; }
    public int ContributionType { get; set; }
    public DateTime Date { get; set; }
}

public class CreateContributionMemberRequest
{
    public int Project { get; set; }
    public int Contributor { get; set; }
    public int ContributionType { get; set; }
    public DateTime Date { get; set; }
}