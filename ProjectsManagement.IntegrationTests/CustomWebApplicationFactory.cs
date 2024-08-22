using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.IO;

public class CustomWebApplicationFactory : WebApplicationFactory<ProjectsManagement.Api.Adapters.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseStartup<ProjectsManagement.Api.Adapters.Program>();
        builder.ConfigureAppConfiguration(config =>
        {
            config.AddJsonFile("appsettings.Test.json");
        });
    }
}