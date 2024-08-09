using Microsoft.OpenApi.Models;
using ProjectsManagement.Api.Adapters.Middlewares;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Identity.Adapters;
using ProjectsManagement.SharedKernel.DependencyInjection.Installer;

namespace ProjectsManagement.Endpoints.Adapters.DependencyInjection;

public class DependencyInjectionInstaller : IDependencyInjectionInstaller
{

    void IDependencyInjectionInstaller.Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddTransient<AuthenticationCheckerMiddleware>();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Projects Management API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
        });
        services.AddHttpClient<IUserIdentityPort, UserIdentityAdapter>();

    }
}
