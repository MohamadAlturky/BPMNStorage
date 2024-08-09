using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectsManagement.Core.Activities.Repositories;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.Core.Tasks.Storage;
using ProjectsManagement.Infrastructure.Repositories;
using ProjectsManagement.SharedKernel.DependencyInjection.Installer;
using ProjectsManagement.Storage.Adapters.Context;

namespace ProjectsManagement.Storage.Adapters.DependencyInjection;

public class DependencyInjectionInstaller : IDependencyInjectionInstaller
{

    void IDependencyInjectionInstaller.Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IActivityRepositoryPort, ActivityRepositoryAdapter>();
        services.AddScoped<IContributionMemberRepositoryPort, ContributionMemberRepositoryAdapter>();
        services.AddScoped<IInvitationRepositoryPort, InvitationRepositoryAdapter>();
        services.AddScoped<IProjectRepositoryPort, ProjectRepositoryAdapter>();
        services.AddScoped<IProjectTaskRepositoryPort, ProjectTaskRepositoryAdapter>();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

    }
}
