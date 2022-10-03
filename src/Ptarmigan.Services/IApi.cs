using System.Collections.Generic;
using Domo;
using Ptarmigan.Utils;

namespace Ptarmigan.Services
{
    public interface IApi : ILogger
    {
        IEnumerable<IService> GetServices();
        IEnumerable<IRepository> GetRepositories();
        void AddService<T>(T service) where T : IService;
        void AddRepository<T>(T repository) where T : IRepository;
        IEventBus EventBus { get; }
    }
}
