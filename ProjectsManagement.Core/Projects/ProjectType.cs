using ProjectsManagement.SharedKernel.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace ProjectsManagement.Core.Projects;

public partial class ProjectType : EntityBase
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
