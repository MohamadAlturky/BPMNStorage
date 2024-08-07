using ProjectsManagement.SharedKernel.Contracts.Entities;

namespace ProjectsManagement.Core.ProjectTasks;

public partial class ProjectTaskStatus : EntityBase
{
    public string Name { get; set; } = null!;
    public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
}
