using System;
using System.Collections.Generic;
using Features.DependencyInjection.Application;
using Features.Observer.Application;

namespace Features.Observer.Main
{
    public class EventService : IService
    {
        private readonly Dictionary<Type, IListener> _listeners = new Dictionary<Type, IListener>();


        public void SendNotification<T>(IEvent generatedEvent)
        {
            var type = typeof(T);
            _listeners.TryGetValue(type, out var listener);
            listener?.ReactionToEvent(generatedEvent);
        }

        public void Register<T>(IListener listener)
        {
            var type = typeof(T);
            _listeners.TryAdd(type, listener);
        }

        public void Reset()
        {
            _listeners.Clear();
        }
    }
}