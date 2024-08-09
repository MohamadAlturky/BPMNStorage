using ProjectsManagement.Core.Common;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Contracts.Entities;

namespace ProjectsManagement.Core.ProjectTasks;

public class ProjectTask : ResourceEntityBase
{

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;


    public int Project { get; set; }
    public int TaskStatus { get; set; }


    public Project ProjectNavigation { get; set; } = null!;
    public ProjectTaskStatus TaskStatusNavigation { get; set; } = null!;

}
