using Microsoft.Extensions.Logging;
using Moq;

namespace ProjectsManagement.UnitTests.Shared;
public class TestLogger<T> : ILogger<T>
{
    private readonly Mock<ILogger<T>> _mockLogger;

    public TestLogger(Mock<ILogger<T>> mockLogger)
    {
        _mockLogger = mockLogger;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        _mockLogger.Object.Log(logLevel, eventId, state, exception, formatter);
    }

    public bool IsEnabled(LogLevel logLevel) => _mockLogger.Object.IsEnabled(logLevel);

    public IDisposable BeginScope<TState>(TState state) => _mockLogger.Object.BeginScope(state);
}