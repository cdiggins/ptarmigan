using System;
using System.Diagnostics;

namespace Ptarmigan.Utils
{
    public enum LogLevel
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
        Fatal = 4,
        Profiling = 5,
    }

    public interface ILogger
    {
        ILogger Log(LogLevel level, string message);
        string Category { get; }
    }

    public static class LoggerExtensions
    {
        public static ILogger Log(this ILogger logger, string message)
            => logger.Log(LogLevel.Info, message);

        public static ILogger LogWarning(this ILogger logger, string message)
            => logger.Log(LogLevel.Warning, message);

        public static ILogger LogDebug(this ILogger logger, string message)
            => logger.Log(LogLevel.Debug, message);

        public static ILogger LogError(this ILogger logger, string message, Exception e)
            => logger.Log(LogLevel.Error, $"{e.Message} {message}");
        
        public static ILogger LogError(this ILogger logger, Exception e)
            => logger.LogError("", e);

        public static ILogger LogError(this ILogger logger, string message)
            => logger.Log(LogLevel.Error, message);

        public static Disposer LogDuration(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Profiling, "STARTED: " + message);
            var sw = Stopwatch.StartNew();
            return new Disposer(() => logger.Log(LogLevel.Profiling, $"COMPLETED in {sw.ElapsedMilliseconds} msec"));
        }

        public static Logger SetWriter(this ILogger logger, ILogWriter writer = null)
            => new Logger(writer, logger.Category);
        
        public static Logger Create(this ILogger logger, string category, ILogWriter writer = null)
            => new Logger(writer ?? new LogWriter(), category);
    }
}
