﻿using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.CQRS;

namespace ProjectsManagement.Contracts.Projects.Queries.GetById;

public class GetProjectByIdQuery : IQuery<Project>
{
    public int Id { get; set; }
}