using Carter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectsManagement.SharedKernel.DependencyInjection.Installer;

namespace ProjectsManagement.Endpoints.Adapters.DependencyInjection;

public class DependencyInjectionInstaller : IDependencyInjectionInstaller
{

    void IDependencyInjectionInstaller.Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();
    }
}
