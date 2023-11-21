using System.Collections.Generic;

namespace CptLost.EventBus
{
    public static class EventBus<T> where T : IBusEvent
    {
        private readonly static HashSet<IEventBusReceiver<T>> _receiversSet = new HashSet<IEventBusReceiver<T>>();

        public static void Register(IEventBusReceiver<T> receiver)
        {
            _receiversSet.Add(receiver);
        }

        public static void Unregister(IEventBusReceiver<T> receiver)
        {
            _receiversSet.Remove(receiver);
        }

        public static void UnregisterAll()
        {
            _receiversSet.Clear();
        }

        public static void Invoke(T busEvent)
        {
            foreach (IEventBusReceiver<T> receiver in _receiversSet)
            {
                receiver.Invoke(busEvent);
            }
        }
    }
}