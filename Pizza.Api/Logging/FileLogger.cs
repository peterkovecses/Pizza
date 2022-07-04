using System.Diagnostics.CodeAnalysis;

namespace Pizza.Api.Logging
{
    public class FileLogger : ILogger
    {
        private readonly FileLoggerProvider _fileLoggerProvider;

        public FileLogger([NotNull] FileLoggerProvider fileLoggerProvider)
        {
            _fileLoggerProvider = fileLoggerProvider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var fullFilePath = _fileLoggerProvider.Options.FolderPath + "/" + _fileLoggerProvider.Options.FilePath.Replace("{date}", DateTimeOffset.UtcNow.ToString("yyyyMMdd"));
            var stackTrace = exception != null ? exception.StackTrace : "";
            var logRecord = $"[{DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss+00:00")}] [{logLevel.ToString()}] {formatter(state, exception)} {stackTrace}";

            using (var streamWriter = new StreamWriter(fullFilePath, true))
            {
                streamWriter.WriteLine(logRecord);
            }
        }
    }
}