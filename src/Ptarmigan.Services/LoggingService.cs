using System;
using System.Diagnostics;
using Domo;
using Ptarmigan.Utils;

namespace Ptarmigan.Services
{
    public class LogEntry
    {
        public LogEntry(string text, int category, DateTimeOffset created)
            => (Text, Category, Created) = (text, category, created);
        public LogEntry() {}
        public string Text;
        public int Category;
        public DateTimeOffset Created;
    }


    public class LogRepo : AggregateRepository<LogEntry>
    {
    }

    public class LoggingService : BaseService, ILogger
    {
        public LogRepo Repo { get; set; }
        public Stopwatch Stopwatch { get; } = Stopwatch.StartNew();

        public LoggingService(IApi api, LogRepo repo)
            : base(api)
            => Repo = repo;

        public ILogger Log(LogLevel level, string message = "")
        {
            Repo.Add(Guid.NewGuid(), new LogEntry(message, (int)level, DateTime.Now));
            Debug.WriteLine(Stopwatch.Elapsed.ToString("hh\\:mm\\:ss\\.ff") + " - " + message);
            return this;
        }

        // TODO: maybe multiple logging services can exist, each with their own repo
        public string Category => "LoggingService";
    }
}
