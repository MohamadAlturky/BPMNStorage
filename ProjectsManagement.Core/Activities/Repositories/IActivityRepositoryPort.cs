using ProjectsManagement.SharedKernel.Ports.Storage;

namespace ProjectsManagement.Core.Activities.Repositories;

public interface IActivityRepositoryPort
    : IBaseRepositoryPort<Activity, ActivityFilter>
{ }