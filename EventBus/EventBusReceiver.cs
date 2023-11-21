using System;

namespace CptLost.EventBus
{
    public interface IEventBusReceiver<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }

        public void Invoke(T busEvent);
    }

    public class EventBusReceiver<T> : IEventBusReceiver<T> where T : IBusEvent
    {
        public Action<T> OnEvent { get; set; } = delegate(T busEvent) { };
        public Action OnEventNoArgs { get; set; } = delegate { };

        public EventBusReceiver(Action<T> onEvent)
        {
            OnEvent = onEvent;
        }

        public EventBusReceiver(Action onEventNoArgs)
        {
            OnEventNoArgs = onEventNoArgs;
        }

        public void Add(Action<T> onEvent)
        {
            OnEvent += onEvent;
        }

        public void Remove(Action<T> onEvent)
        {
            OnEvent -= onEvent;
        }

        public void Add(Action onEvent)
        {
            OnEventNoArgs += onEvent;
        }

        public void Remove(Action onEvent)
        {
            OnEventNoArgs -= onEvent;
        }

        public void Invoke(T busEvent)
        {
            OnEventNoArgs.Invoke();
            OnEvent.Invoke(busEvent);
        }
    }
}