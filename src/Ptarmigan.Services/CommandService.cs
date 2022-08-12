using System;
using System.Collections.Generic;
using System.Text;

namespace Ptarmigan.Services
{
    public class CommandEvent
    {
        public string Name;
        public object Arg;
    }

    public class CommandService
    {
        private IEventBus _bus;

        public CommandService(IEventBus bus)
            => _bus = bus;

        public void RegisterCommand(string name) 
        { }
        public void DisableCommand(string name)
        { }
        public void EnableCommand(string name)
        { }
        public void InvokeCommand(string name, object arg)
        { }

        public void SubscribeCommand(string name, ISubscriber<CommandEvent> subscriber)
        {
            // TODO: I need a way of filtering commands 
            _bus.Subscribe<CommandEvent>(subscriber);
        }
    }
}
