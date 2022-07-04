namespace Pizza.Api.Logging
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddFileLogger(this IServiceCollection services, Action<FileLoggerOptions> configure)
        {
            services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
            services.Configure(configure);

            return services;
        }
    }
}
