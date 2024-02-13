using System;

namespace CptLost.EventBus
{
    public interface IEventBusReceiver<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }

        public void Invoke(T busEvent);
    }
}