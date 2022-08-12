using System;
using System.Collections.Generic;

namespace Ptarmigan.Services
{
    public interface IApi : IServiceProvider
    {
        T AddService<T>(T service) where T : class;
        IEnumerable<object> GetServices();
        TService GetService<TService>();
        IEventBus EventBus { get; }
    }
}
