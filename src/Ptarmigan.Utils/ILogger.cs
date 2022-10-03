using System;
using System.Diagnostics;

namespace Ptarmigan.Utils
{
    public enum LogCategory
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
        ILogger Log(int category, string message, params object[] args);
    }

    public class DebugLogger : ILogger
    {
        public ILogger Log(int category, string message, params object[] args)
        {
            Debug.WriteLine(message, args);
            return this;
        }
    }

    public static class LoggerExtensions
    {
        public static ILogger Log(this ILogger logger, LogCategory category, string message, params object[] args)
            => logger.Log((int)category, message, args);

        public static ILogger Log(this ILogger logger, string message, params object[] args)
            => logger.Log(LogCategory.Info, message, args);

        public static ILogger LogWarning(this ILogger logger, string message, params object[] args)
            => logger.Log(LogCategory.Warning, message, args);

        public static ILogger LogDebug(this ILogger logger, string message, params object[] args)
#if DEBUG
            => logger.Log(LogCategory.Debug, message, args);
#else
            => logger;
#endif

        public static ILogger LogError(this ILogger logger, string message, Exception e, params object[] args)
            => logger.Log(LogCategory.Error, $"ERROR {e.Message} " + message, args);
        
        public static ILogger LogError(this ILogger logger, Exception e, params object[] args)
            => logger.LogError("", e, args);

        public static Disposer LogDuration(this ILogger logger, string message)
        {
            logger.Log(LogCategory.Profiling, "STARTED: " + message);
            var sw = Stopwatch.StartNew();
            return new Disposer(() => logger.Log(LogCategory.Profiling, $"COMPLETED in {sw.ElapsedMilliseconds} msec"));
        }
    }
}
