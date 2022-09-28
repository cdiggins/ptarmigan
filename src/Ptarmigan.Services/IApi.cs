using System.Collections.Generic;
using Domo;

namespace Ptarmigan.Services
{
    public interface IApi
    {
        IEnumerable<IService> GetServices();
        IEnumerable<IRepository> GetRepositories();
        void AddService<T>(T service) where T : IService;
        void AddRepository<T>(T repository) where T : IRepository;
        IEventBus EventBus { get; }
    }
}
