using Example.EventDriven.Domain.Gateways.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Infrastructure.Logger
{
    [ExcludeFromCodeCoverage]
    public class ConsoleLoggerManager : ILoggerManager
    {
        private readonly ILogger<ConsoleLoggerManager> _logger;
        private readonly LoggerManagerSeverity _minLevel;

        public ConsoleLoggerManager(ILogger<ConsoleLoggerManager> logger, IConfiguration configuration)
        {
            _logger = logger;
            if (!Enum.TryParse(configuration.GetValue("Logging:LogLevel:Default", LogLevel.Debug.ToString()).ToUpper(), out _minLevel))
                throw new ArgumentException("Invalid log level");
        }

        public void LogException(string message, LoggerManagerSeverity severity, Exception exception = default) =>
            LogException(message, severity, exception, Array.Empty<(string name, string value)>());

        public void LogException(string message, LoggerManagerSeverity severity, Exception exception = default, params (string name, object value)[] parameters)
        {
            var newParameters = AddExceptionToParameters(exception, parameters);

            Log(message, severity, newParameters);
        }

        public void LogException(string message, LoggerManagerSeverity severity, Exception exception = default, params (string name, string value)[] parameters)
        {
            var newParameters = AddExceptionToParameters(exception, parameters);

            Log(message, severity, newParameters);
        }

        public void Log(string message, LoggerManagerSeverity severity)
        {
            if (_minLevel > severity)
                return;

            Log(message, severity, Array.Empty<(string, string)>());
        }

        public void Log(string message, LoggerManagerSeverity severity, params (string name, object value)[] parameters)
        {
            if (_minLevel > severity)
                return;

            parameters ??= Array.Empty<(string name, object value)>();
            var parametersAsString = new (string name, string value)[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
                parametersAsString[i] = (parameters[i].name, JsonConvert.SerializeObject(parameters[i].value));

            Log(message, severity, parametersAsString);
        }

        [SuppressMessage("Usage", "CA2254", Justification = "")]
        public void Log(string message, LoggerManagerSeverity severity, params (string name, string value)[] parameters)
        {
            if (_minLevel > severity)
                return;

            switch (severity)
            {
                case LoggerManagerSeverity.INFORMATION:
                    _logger.LogInformation(message, parameters);
                    break;
                case LoggerManagerSeverity.WARNING:
                    _logger.LogWarning(message, parameters);
                    break;
                case LoggerManagerSeverity.ERROR:
                    _logger.LogError(message, parameters);
                    break;
                case LoggerManagerSeverity.CRITICAL:
                    _logger.LogCritical(message, parameters);
                    break;
                default:
                    _logger.LogDebug(message, parameters);
                    break;
            }
        }

        private static (string name, object value)[] AddExceptionToParameters(Exception exception, (string name, object value)[] parameters)
        {
            var newParameters = new (string name, object value)[parameters.Length + 1];

            parameters.CopyTo(newParameters, 0);

            if (exception != null)
                newParameters[^1] = (nameof(exception), exception);

            return newParameters;
        }

        private static (string name, string value)[] AddExceptionToParameters(Exception exception, (string name, string value)[] parameters)
        {
            var newParameters = new (string name, string value)[parameters.Length + 1];

            parameters.CopyTo(newParameters, 0);

            if (exception != null)
                newParameters[^1] = (nameof(exception), JsonConvert.SerializeObject(exception));
            return newParameters;
        }

    }
}
