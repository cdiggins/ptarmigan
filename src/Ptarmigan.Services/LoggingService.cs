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

    public class LoggingService : ILogger
    {
        public LogRepo Repo { get; set; }
        public Stopwatch Stopwatch { get; } = Stopwatch.StartNew();

        public LoggingService(LogRepo repo)
            => Repo = repo;

        public ILogger Log(int category, string message = "", params object[] args)
        {
            Repo.Add(Guid.NewGuid(), new LogEntry(message, category, DateTime.Now));
            Debug.WriteLine(Stopwatch.Elapsed.ToString("hh\\:mm\\:ss\\.ff") + " - " + message);
            return this;
        }
    }
}
