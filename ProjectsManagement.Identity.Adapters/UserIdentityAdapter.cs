using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Core.Contributions;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

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

        //throw new HttpRequestException($"Error fetching user ID. Status code: {response.StatusCode}");
        throw new UnauthorizedAccessException($"Unauthorized");
    }

    public async Task<HashSet<ContributorInfo>> GetUsersAsync(HashSet<int> ids)
    {
        if (ids == null || !ids.Any())
        {
            throw new ArgumentException("List of IDs cannot be null or empty", nameof(ids));
        }

        string token = _extractor.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var jsonContent = new StringContent(JsonSerializer.Serialize(ids), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}/users", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("{content}",content);
            HashSet<ContributorInfo>? responses = JsonSerializer.Deserialize<HashSet<ContributorInfo>>(content);

            return responses;
        }

        _logger.LogError("Failed to fetch users. Status code: {StatusCode}", response.StatusCode);
        throw new HttpRequestException($"Error fetching users. Status code: {response.StatusCode}");
    }
}
