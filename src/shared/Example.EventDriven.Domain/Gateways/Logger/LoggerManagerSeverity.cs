using Microsoft.Extensions.Logging;

namespace Example.EventDriven.Domain.Gateways.Logger
{
    public enum LoggerManagerSeverity
    {
        DEBUG = LogLevel.Debug,
        INFORMATION = LogLevel.Information,
        WARNING = LogLevel.Warning,
        ERROR = LogLevel.Error,
        CRITICAL = LogLevel.Critical,
    }
}
