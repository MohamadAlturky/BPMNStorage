﻿using ProjectsManagement.Core.Invitations;
using ProjectsManagement.SharedKernel.CQRS;

namespace ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;

public class GetInvitationByIdQuery : IQuery<Invitation>
{
    public int Id { get; set; }
}
