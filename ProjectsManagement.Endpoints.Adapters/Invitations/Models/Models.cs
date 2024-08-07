using ProjectsManagement.Core.Invitations;

namespace ProjectsManagement.API.Endpoints.Invitations;

public class GetInvitationByIdRequest
{
    public int Id { get; set; }
}

public class FilterInvitationRequest
{
    public InvitationFilter Filter { get; set; }
}

public class UpdateInvitationRequest
{
    public int Id { get; set; }
    public int InvitationStatus { get; set; }
    public int Contributor { get; set; }
    public int Project { get; set; }
    public string Message { get; set; } = null!;
    public DateTime Date { get; set; }
}

public class CreateInvitationRequest
{
    public string Message { get; set; } = null!;
    public DateTime Date { get; set; }
    public int Contributor { get; set; }
    public int Project { get; set; }
    public int InvitationStatus { get; set; }
}