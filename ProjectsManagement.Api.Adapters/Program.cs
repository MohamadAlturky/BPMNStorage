using ProjectsManagement.Application.Users;
using ProjectsManagement.Identity.Adapters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IUserIdentityPort, UserIdentityAdapter>();
builder.Services.AddScoped<IUserIdentityPort, UserIdentityAdapter>();
builder.Services.AddScoped(typeof(TokenExtractor));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

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

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
