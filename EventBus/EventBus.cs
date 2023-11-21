using System.Collections.Generic;

namespace CptLost.EventBus
{
    public static class EventBus<T> where T : IBusEvent
    {
        private readonly static HashSet<IEventBusReceiver<T>> _receiversDictionary = new HashSet<IEventBusReceiver<T>>();

        public static void Register(IEventBusReceiver<T> receiver)
        {
            _receiversDictionary.Add(receiver);
        }

        public static void Unregister(IEventBusReceiver<T> receiver)
        {
            _receiversDictionary.Remove(receiver);
        }

        public static void UnregisterAll()
        {
            _receiversDictionary.Clear();
        }

        public static void Invoke(T busEvent)
        {
            foreach (IEventBusReceiver<T> receiver in _receiversDictionary)
            {
                receiver.Invoke(busEvent);
            }
        }
    }
}