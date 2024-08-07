using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Ports.Storage;

namespace ProjectsManagement.Core.Projects.Repositories;

public interface IProjectRepositoryPort
    : IBaseRepositoryPort<Project, ProjectFilter>
{ }