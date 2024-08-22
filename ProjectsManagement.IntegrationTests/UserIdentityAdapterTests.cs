using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Identity.Adapters;

namespace ProjectsManagement.IntegrationTests;

public class UserIdentityAdapterTests
{
    private Mock<ILogger<UserIdentityAdapter>> _mockLogger;
    private Mock<ITokenExtractor> _mockTokenExtractor;
    private HttpClient _httpClient = new HttpClient();
    private UserIdentityAdapter _adapter;
    private IConfiguration _configuration;

    [SetUp]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger<UserIdentityAdapter>>();
        _mockTokenExtractor = new Mock<ITokenExtractor>();

        _configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();
        _adapter = new UserIdentityAdapter(_httpClient, _configuration, _mockLogger.Object, _mockTokenExtractor.Object);
    }

    [Test]
    public async Task GetUsersAsync_ValidIds_ReturnsContributorInfoSet()
    {
        // Arrange
        var ids = new HashSet<int> { 1 };
        var contributors = new HashSet<ContributorInfo>
        {
            new ContributorInfo { Id = 1, UserName = "mmm@hiast.com",Email="mmm@hiast.com" },
        };


        // Act
        HashSet<ContributorInfo> result = await _adapter.GetUsersAsync(ids);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(contributors.Count, result.Count);
        Console.WriteLine($"result.Count {result.Count}");
        foreach (var contributor in result)
        {
            Console.WriteLine($"Id: {contributor.Id}, UserName: {contributor.UserName}, Email: {contributor.Email}");
        }

        foreach(var contributorInfo in contributors)
        {
            var info = result.Where(e=>e.Id==contributorInfo.Id).FirstOrDefault();
            if(info is null)
            {
                throw new Exception("no matching");
            }
            if(info.UserName != contributorInfo.UserName)
            {
                throw new Exception("no matching in username");
            }
            if (info.Email!= contributorInfo.Email)
            {
                throw new Exception("no matching in email");
            }
        }
    }

}
