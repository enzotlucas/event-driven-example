namespace Example.EventDriven.Domain.Gateways.Logger
{
    public interface ILoggerManager
    {
        void LogException(string message, LoggerManagerSeverity severity, Exception exception = default);
        void LogException(string message, LoggerManagerSeverity severity, Exception exception = default, params (string name, object value)[] parameters);
        void LogException(string message, LoggerManagerSeverity severity, Exception exception = default, params (string name, string value)[] parameters);
        void Log(string message, LoggerManagerSeverity severity);
        void Log(string message, LoggerManagerSeverity severity, params (string name, string value)[] parameters);
        void Log(string message, LoggerManagerSeverity severity, params (string name, object value)[] parameters);
    }
}
