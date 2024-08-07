﻿using ProjectsManagement.Core.Activities;
using ProjectsManagement.SharedKernel.CQRS;

namespace ProjectsManagement.Contracts.Activities.Commands.Create;

public class CreateActivityCommand : ICommand<Activity>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }


    public int Project { get; set; }
    public int ActivityType { get; set; }
    public int ActivityResourceType { get; set; }

}