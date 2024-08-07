using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Ports.Storage;

namespace ProjectsManagement.Core.Tasks.Storage;

public interface IProjectTaskRepositoryPort 
    : IBaseRepositoryPort<ProjectTask, ProjectTaskFilter> { }