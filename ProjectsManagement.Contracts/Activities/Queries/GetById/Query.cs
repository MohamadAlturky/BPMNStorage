using ProjectsManagement.Core.Activities;
using ProjectsManagement.SharedKernel.CQRS;

namespace ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;

public class GetActivityByIdQuery : IQuery<Activity>
{
    public int Id { get; set; }
}
