using ProjectsManagement.Application.Users;
using System.Net;
using System.Text.Json;

namespace ProjectsManagement.Api.Adapters.Middlewares;
public class AuthenticationCheckerMiddleware : IMiddleware
{
    private readonly IUserIdentityPort _identityPort;

    public AuthenticationCheckerMiddleware(IUserIdentityPort identityPort)
    {
        _identityPort=identityPort;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await _identityPort.GetUserIdAsync();
        }
        catch(Exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = HttpStatusCode.Unauthorized,
                message = "Unauthorized access"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        await next(context);

    }
}