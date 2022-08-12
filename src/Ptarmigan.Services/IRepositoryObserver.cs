using System.Collections.Generic;
using System.Linq;
using Domo;
using Ptarmigan.Utils;

namespace Ptarmigan.Services
{

    public class RepositoryChangedEvent
    {
        public RepositoryChangedEvent(RepositoryChangeArgs args)
            => Args = args;

        public RepositoryChangeArgs Args { get; }
    }

    public class ModelChangedEvent<T> : RepositoryChangedEvent
    {
        public ModelChangedEvent(RepositoryChangeArgs args) : base(args)
        {
        }

        public IModel<T> Model => ((IRepository<T>)Args.Repository).GetModels().FirstOrDefault();
    }

    public class ModelsChangedEvent<T> : RepositoryChangedEvent
    {
        public ModelsChangedEvent(RepositoryChangeArgs args) : base(args)
        {
        }

        public IReadOnlyList<IModel<T>> Models => ((IRepository<T>)Args.Repository).GetModels();
    }

    public interface IRepositoryObserver<T> : IDisposingNotifier, ISubscriber<ModelChangedEvent<T>>
    {
    }
}