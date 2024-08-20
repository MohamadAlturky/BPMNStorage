using Carter;
using ProjectsManagement.Api.Adapters.AssemblyReference;
using ProjectsManagement.Api.Adapters.Middlewares;
using ProjectsManagement.Application.AssemblyReference;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Endpoints.Adapters.AssemblyReference;
using ProjectsManagement.Identity.Adapters.AssemblyReference;
using ProjectsManagement.SharedKernel.DependencyInjection.Scanner;
using ProjectsManagement.Storage.Adapters.AssemblyReference;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


Assembly[] assemblies = [
        typeof(ApplicationAssemblyReference).Assembly,
        typeof(StorageAdaptersAssemblyReference).Assembly,
        typeof(EndpointsAssemblyReference).Assembly,
        typeof(IdentityAssemblyReference).Assembly,
        typeof(APIAssemblyReference).Assembly,
];

builder.Services.RegisterServices(builder.Configuration, assemblies);



var app = builder.Build();

//app.UseMiddleware<AuthenticationCheckerMiddleware>();


app.UseSwagger();
app.UseSwaggerUI();

app.MapCarter();

app.UseHttpsRedirection();


app.MapGet("/api/header", (HttpContext httpContext) =>
{
    const string headerName = "HeaderName";
    if (httpContext.Request.Headers.TryGetValue(headerName, out var headerValue))
    {
        return Results.Ok(headerValue.ToString());
    }
    return Results.BadRequest($"Header '{headerName}' not found");
});
app.MapGet("/api/userId", async (IUserIdentityPort port) =>
{
    try
    {
        int id = await port.GetUserIdAsync();
        return Results.Ok(id);
    }
    catch (Exception)
    {

    return Results.BadRequest($"Error");
    }
});
app.Run();
