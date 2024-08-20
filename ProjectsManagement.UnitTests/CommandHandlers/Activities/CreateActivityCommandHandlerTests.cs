using Moq;
using NUnit.Framework;
using ProjectsManagement.Application.Activities.Commands.Create;
using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.Activities.Repositories;
using Microsoft.Extensions.Logging;
using ProjectsManagement.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectsManagement.Contracts.Activities.Commands.Create;
using ProjectsManagement.UnitTests.Shared;

namespace ProjectsManagement.Tests.Application.Activities.Commands.Create
{
    public class CreateActivityCommandHandlerTests
    {
        private Mock<IActivityRepositoryPort> _mockActivityRepository;
        private Mock<ILogger<CreateActivityCommandHandler>> _mockLogger;
        private CreateActivityCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockActivityRepository = new Mock<IActivityRepositoryPort>();
            _mockLogger = new Mock<ILogger<CreateActivityCommandHandler>>();

            _handler = new CreateActivityCommandHandler(
                _mockActivityRepository.Object,
                new TestLogger<CreateActivityCommandHandler>(_mockLogger));
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenActivityIsCreated()
        {
            // Arrange
            var command = new CreateActivityCommand
            {
                Name = "Test Activity",
                Description = "Activity Description",
                Date = DateTime.UtcNow,
                Project = 1,
                ActivityType = 1,
                ActivityResourceType = 1,
                BaseOn = new List<int>() 
            };

            var activity = new Activity { Id = 1, Name = command.Name, Description = command.Description, Date = command.Date, Project = command.Project, ActivityType = command.ActivityType, ActivityResourceType = command.ActivityResourceType };
            _mockActivityRepository.Setup(repo => repo.AddAsync(It.IsAny<Activity>())).ReturnsAsync(activity);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Value, Is.EqualTo(activity));
            _mockActivityRepository.Verify(repo => repo.AddAsync(It.IsAny<Activity>()), Times.Once);
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
            var command = new CreateActivityCommand
            {
                Name = "",
                Description = "Activity Description",
                Date = DateTime.UtcNow,
                Project = 1,
                ActivityType = 1,
                ActivityResourceType = 1
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.That(result.Error.Code, Is.EqualTo("Activity.NameRequired"));
            _mockActivityRepository.Verify(repo => repo.AddAsync(It.IsAny<Activity>()), Times.Never);
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
            var command = new CreateActivityCommand
            {
                Name = "Test Activity",
                Description = "Activity Description",
                Date = DateTime.UtcNow,
                Project = 1,
                ActivityType = 1,
                ActivityResourceType = 1
            };

            var exception = new Exception("Database error");
            _mockActivityRepository.Setup(repo => repo.AddAsync(It.IsAny<Activity>())).ThrowsAsync(exception);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.That(result.Error.Code, Is.EqualTo("Activity.CreationFailed"));

            _mockLogger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.Is<Exception>(ex => ex.Message == exception.Message),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }
    }
}