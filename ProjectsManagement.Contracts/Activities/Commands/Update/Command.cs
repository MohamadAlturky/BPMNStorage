using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.ProjectTasks.Commands.Update;


public class UpdateActivityCommand : ICommand<Activity>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }


    public int Project { get; set; }
    public int ActivityType { get; set; }
    public int ActivityResourceType { get; set; }
}