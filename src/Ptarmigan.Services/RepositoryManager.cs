using System;
using System.Collections.Generic;
using System.Linq;
using Domo;

namespace Ptarmigan.Services
{
    public interface IRepositoryManager
    {
        IEnumerable<IRepository> GetRepositories();
    }

    public class RepositoryManager : IRepositoryManager
    {
        public List<IRepository> Repositories { get; } = new List<IRepository>();

        public IEnumerable<IRepository> GetRepositories()
            => Repositories;

        public void AddRepository(IRepository repository)
            => Repositories.Add(repository);
    }

    public interface IAggregateRepositoryObserver<T> : IRepositoryObserver<T>
    {
        void OnModelsChanged(IEnumerable<T> models, RepositoryChangeArgs args);
    }

    public static class RepositoryManagerExtensions
    {
        public static ISingletonRepository<T> GetSingletonRepository<T>(this IRepositoryManager manager)
            => manager.GetRepositories().OfType<ISingletonRepository<T>>().FirstOrDefault();

        public static IAggregateRepository<T> GetAggregateRepository<T>(this IRepositoryManager manager)
            => manager.GetRepositories().OfType<IAggregateRepository<T>>().FirstOrDefault();
    }
}
