using Microsoft.AspNetCore.Http;
namespace ProjectsManagement.Identity.Adapters;

public class TokenExtractor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenExtractor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string GetToken()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return string.Empty;
        }

        var authHeader = httpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return string.Empty;
        }

        return authHeader.Substring("Bearer ".Length).Trim();
    }
}