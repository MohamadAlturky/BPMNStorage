using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectsManagement.Application.AssemblyReference;
using ProjectsManagement.Application.PiplineBehaviors;
using ProjectsManagement.SharedKernel.DependencyInjection.Installer;

namespace ProjectsManagement.Storage.Adapters.DependencyInjection;

public class DependencyInjectionInstaller : IDependencyInjectionInstaller
{

    void IDependencyInjectionInstaller.Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        }).AddScoped(typeof(IPipelineBehavior<,>), typeof(ReadAccessControlPolicyPiplineBehavior<,>))
        .AddScoped(typeof(IPipelineBehavior<,>), typeof(WriteAccessControlPolicyPiplineBehavior<,>));

    }
}
