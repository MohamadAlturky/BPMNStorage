using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Application.Users;
using System.Net.Http.Headers;

namespace ProjectsManagement.Identity.Adapters;
public class UserIdentityAdapter : IUserIdentityPort
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = string.Empty;
    private readonly ILogger<UserIdentityAdapter> _logger;
    private readonly TokenExtractor _extractor;
    public UserIdentityAdapter(HttpClient httpClient, IConfiguration configuration, ILogger<UserIdentityAdapter> logger, TokenExtractor extractor)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["UserApi:BaseUrl"];
        _logger = logger;
        _extractor = extractor;
    }

    public async Task<int> GetUserIdAsync()
    {
        string token = _extractor.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"{_baseUrl}/userId/");

        if (response.IsSuccessStatusCode)
        {
            string Id = await response.Content.ReadAsStringAsync();
            return int.Parse(Id);
        }

        throw new HttpRequestException($"Error fetching user ID. Status code: {response.StatusCode}");
    }
}