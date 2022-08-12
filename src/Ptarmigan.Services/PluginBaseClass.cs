using System;
using System.Collections.Generic;
using Domo;

namespace Ptarmigan.Services
{
    public class PluginBaseClass : IPlugin
    {
        public IApi Api { get; private set; }

        public virtual string Name => nameof(PluginBaseClass);

        public virtual void Initialize(IApi api)
            => Api = api;

        public event EventHandler Disposing;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disposing?.Invoke(this, EventArgs.Empty);
            }
            Api = null;
            Disposing = null;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        public void OnModelChanged<T>(Action<IModel<T>> action)
            => Api.EventBus.OnModelChanged(action, this);

        public void OnModelsChanged<T>(Action<IReadOnlyList<IModel<T>>> action)
            => Api.EventBus.OnModelsChanged(action, this);

    }
}
