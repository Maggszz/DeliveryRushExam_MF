using System;
using System.Collections.Generic;

namespace DeliveryRushExam.Save
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public static void Register<TService>(TService service)
        {
            Type type = typeof(TService);

            if (Services.ContainsKey(type))
            {
                Services[type] = service;
                return;
            }

            Services.Add(type, service);
        }

        public static TService Get<TService>()
        {
            Type type = typeof(TService);

            if (!Services.TryGetValue(type, out object service))
            {
                throw new InvalidOperationException("Service not registered: " + type.Name);
            }

            return (TService)service;
        }

        public static bool TryGet<TService>(out TService service)
        {
            if (Services.TryGetValue(typeof(TService), out object value))
            {
                service = (TService)value;
                return true;
            }

            service = default;
            return false;
        }

        public static void Clear()
        {
            Services.Clear();
        }
    }
}
