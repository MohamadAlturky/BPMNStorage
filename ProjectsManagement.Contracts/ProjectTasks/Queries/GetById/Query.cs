﻿using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;

public class GetProjectTaskByIdQuery : IQuery<ProjectTask>
{
    public int Id { get; set; }
    public AccessControlCriteria Criteria()
    {
        return new();
    }
}
