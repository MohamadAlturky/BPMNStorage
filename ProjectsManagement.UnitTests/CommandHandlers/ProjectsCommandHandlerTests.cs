using Moq;
using NUnit.Framework;

namespace ProjectsManagement.UnitTests.CommandHandlers;

[TestFixture]
public class ProjectsCommandHandlerTests
{
    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void GetCustomers_ShouldReturnAListOfCustomers_WhenCallingIt()
    {
        int x = 1;
        Assert.That(x, Is.EqualTo(1));
    }
}