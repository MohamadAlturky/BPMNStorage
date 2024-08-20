using Moq;
using NUnit.Framework;
using ProjectsManagement.Application.Invitations.Commands.Create;
using ProjectsManagement.Core.Invitations;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Create;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.UnitTests.Shared;

namespace ProjectsManagement.Tests.Application.Invitations.Commands.Create
{
    public class CreateInvitationCommandHandlerTests
    {
        private Mock<IInvitationRepositoryPort> _mockInvitationRepository;
        private Mock<ILogger<CreateInvitationCommandHandler>> _mockLogger;
        private CreateInvitationCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockInvitationRepository = new Mock<IInvitationRepositoryPort>();
            _mockLogger = new Mock<ILogger<CreateInvitationCommandHandler>>();

            _handler = new CreateInvitationCommandHandler(
                _mockInvitationRepository.Object,
                new TestLogger<CreateInvitationCommandHandler>(_mockLogger));
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenInvitationIsCreated()
        {
            // Arrange
            var command = new CreateInvitationCommand
            {
                Message = "Test Invitation",
                Date = DateTime.UtcNow,
                Contributor = 1,
                Project = 1,
                By = 1,
                InvitationStatus = 1
            };

            var invitation = new Invitation { Id = 1, Message = command.Message, Date = command.Date, Contributor = command.Contributor, Project = command.Project, By = command.By, InvitationStatus = command.InvitationStatus };
            _mockInvitationRepository.Setup(repo => repo.AddAsync(It.IsAny<Invitation>())).ReturnsAsync(invitation);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Value, Is.EqualTo(invitation));
            _mockInvitationRepository.Verify(repo => repo.AddAsync(It.IsAny<Invitation>()), Times.Once);
            _mockLogger.Verify(logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenValidationFails()
        {
            // Arrange
            var command = new CreateInvitationCommand
            {
                Message = "",
                Date = DateTime.UtcNow,
                Contributor = 1,
                Project = 1,
                By = 1,
                InvitationStatus = 1
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.That(result.Error.Code, Is.EqualTo("Invitation.MessageRequired"));
            _mockInvitationRepository.Verify(repo => repo.AddAsync(It.IsAny<Invitation>()), Times.Never);
            _mockLogger.Verify(logger => logger.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenExceptionOccurs()
        {
            // Arrange
            var command = new CreateInvitationCommand
            {
                Message = "Test Invitation",
                Date = DateTime.UtcNow,
                Contributor = 1,
                Project = 1,
                By = 1,
                InvitationStatus = 1
            };

            var exception = new Exception("Database error");
            _mockInvitationRepository.Setup(repo => repo.AddAsync(It.IsAny<Invitation>())).ThrowsAsync(exception);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.That(result.Error.Code, Is.EqualTo("Invitation.CreationFailed"));
            _mockLogger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.Is<Exception>(ex => ex.Message == exception.Message),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }
    }
}