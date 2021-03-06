using Microsoft.Extensions.Options;

namespace Pizza.Api.Logging
{
    [ProviderAlias("FileLogging")]
    public class FileLoggerProvider : ILoggerProvider
    {
        public readonly FileLoggerOptions Options;

        public FileLoggerProvider(IOptions<FileLoggerOptions> options)
        {
            Options = options.Value;

            if (!Directory.Exists(Options.FolderPath))
                Directory.CreateDirectory(Options.FolderPath);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }

        public void Dispose()
        {

        }
    }
}
