using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Identity.Adapters;
using ProjectsManagement.SharedKernel.DependencyInjection.Installer;

namespace ProjectsManagement.Storage.Adapters.DependencyInjection;

public class DependencyInjectionInstaller : IDependencyInjectionInstaller
{

    void IDependencyInjectionInstaller.Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserIdentityPort, UserIdentityAdapter>();
        services.AddScoped<ITokenExtractor, TokenExtractor>();
    }
}
