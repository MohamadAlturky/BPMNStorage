using Moq;
using NUnit.Framework;
using ProjectsManagement.Application.Projects.Commands.Create;
using ProjectsManagement.Core.Projects;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Contracts.Projects.Commands.Create;
using ProjectsManagement.Core.Contributions;

namespace ProjectsManagement.Tests.Application.Projects.Commands.Create
{
    public class CreateProjectCommandHandlerTests
    {
        private Mock<IProjectRepositoryPort> _mockProjectRepository;
        private Mock<IUserIdentityPort> _mockIdentityPort;
        private Mock<IContributionMemberRepositoryPort> _mockMemberRepository;
        private Mock<ILogger<CreateProjectCommandHandler>> _mockLogger;
        private CreateProjectCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockProjectRepository = new Mock<IProjectRepositoryPort>();
            _mockIdentityPort = new Mock<IUserIdentityPort>();
            _mockMemberRepository = new Mock<IContributionMemberRepositoryPort>();
            _mockLogger = new Mock<ILogger<CreateProjectCommandHandler>>();

            _handler = new CreateProjectCommandHandler(
                _mockProjectRepository.Object,
                _mockIdentityPort.Object,
                _mockMemberRepository.Object,
                _mockLogger.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenProjectIsCreated()
        {
            // Arrange
            var command = new CreateProjectCommand
            {
                Name = "Test Project",
                Description = "Project Description",
                ProjectType = 1
            };

            var project = new Project { Id = 1, Name = command.Name, Description = command.Description, ProjectType = command.ProjectType };
            _mockProjectRepository.Setup(repo => repo.AddAsync(It.IsAny<Project>())).ReturnsAsync(project);
            _mockIdentityPort.Setup(id => id.GetUserIdAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Value, Is.EqualTo(project));
            _mockProjectRepository.Verify(repo => repo.AddAsync(It.IsAny<Project>()), Times.Once);
            _mockMemberRepository.Verify(repo => repo.AddAsync(It.IsAny<ContributionMember>()), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenValidationFails()
        {
            // Arrange
            var command = new CreateProjectCommand
            {
                Name = "",
                Description = "Project Description",
                ProjectType = 1
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.That(result.Error.Code, Is.EqualTo("Project.NameRequired"));
            _mockProjectRepository.Verify(repo => repo.AddAsync(It.IsAny<Project>()), Times.Never);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenExceptionOccurs()
        {
            // Arrange
            var command = new CreateProjectCommand
            {
                Name = "Test Project",
                Description = "Project Description",
                ProjectType = 1
            };

            var exception = new Exception("Database error");
            _mockProjectRepository.Setup(repo => repo.AddAsync(It.IsAny<Project>())).ThrowsAsync(exception);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.That(result.Error.Code, Is.EqualTo("Project.CreationFailed"));

            _mockLogger.Verify(logger => logger.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }
    }
}