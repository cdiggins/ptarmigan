using System;
using Ptarmigan.Utils;

namespace Ptarmigan.Services
{
    public class Subscriber<T> : DisposingNotifier, ISubscriber<T>
    {
        public Subscriber(Action<T> action, IDisposingNotifier notifier = null)
            : base(notifier)
        {
            Action = action;
        }

        public Action<T> Action { get; }

        public void OnEvent(T evt)
            => Action?.Invoke(evt);
    }
}