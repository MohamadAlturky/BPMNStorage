using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Core.Invitations;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Contracts.Entities;

namespace ProjectsManagement.Core.Projects;

public  class Project : AccessControlledEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public int ProjectType { get; set; }
    public  ProjectType ProjectTypeNavigation { get; set; } = null!;


    public  ICollection<Activity> Activities { get; set; } = new List<Activity>();
    public  ICollection<ContributionMember> ContributionMembers { get; set; } = new List<ContributionMember>();
    public  ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
    public  ICollection<ProjectTask> Tasks { get; set; } = [];
}
