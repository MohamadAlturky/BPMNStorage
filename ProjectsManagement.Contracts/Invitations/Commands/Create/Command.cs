﻿using ProjectsManagement.Core.Invitations;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.CQRS;

namespace ProjectsManagement.Contracts.ProjectTasks.Commands.Create;

public class CreateInvitationCommand : ICommand<Invitation>
{
    public string Message { get; set; } = null!;
    public DateTime Date { get; set; }

    public int By { get; set; }
    public int Contributor { get; set; }
    public int Project { get; set; }
    public int InvitationStatus { get; set; }
    public AccessControlCriteria Criteria()
    {
        return new();
    }
}