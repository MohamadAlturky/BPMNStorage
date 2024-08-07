using Carter;
using ProjectsManagement.Application.AssemblyReference;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Endpoints.Adapters.AssemblyReference;
using ProjectsManagement.Identity.Adapters;
using ProjectsManagement.Identity.Adapters.AssemblyReference;
using ProjectsManagement.SharedKernel.DependencyInjection.Scanner;
using ProjectsManagement.Storage.Adapters.AssemblyReference;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IUserIdentityPort, UserIdentityAdapter>();

Assembly[] assemblies = [
        typeof(ApplicationAssemblyReference).Assembly,
        typeof(StorageAdaptersAssemblyReference).Assembly,
        typeof(EndpointsAssemblyReference).Assembly,
        typeof(IdentityAssemblyReference).Assembly
];

builder.Services.RegisterServices(builder.Configuration, assemblies);



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
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
