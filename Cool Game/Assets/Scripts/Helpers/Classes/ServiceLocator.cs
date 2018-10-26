using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers.Classes
{
    public class ServiceLocator
    {
        private static readonly List<object> _services = new List<object>();

        public static void AddService<T>(T t)
        {
            foreach (object o in _services)
            {
                if (o is T || o.GetType().IsAssignableFrom(typeof(T)))
                    throw new ArgumentException(
                        $"Cannot add: {typeof(T).FullName}, because there already is an {o.GetType().FullName}");
            }
            _services.Add(t);
        }

        public static void AddOrReplaceService<T>(T service)
        {
            List<T> foundServices = new List<T>();
            for (int i = 0; i < _services.Count; i++)
            {
                if (!(_services[i] is T) && !_services[i].GetType().IsAssignableFrom(typeof(T)))
                    continue;
                foundServices.Add((T) _services[i]);
                if (foundServices.Count == 1)
                    _services[i] = service;
            }
            switch (foundServices.Count)
            {
                case 0:
                    AddService(service);
                    break;
                case 1:
                    return;
                default:
                    throw new InvalidOperationException("Multiple services found with " + typeof(T).Name
                                                        + foundServices.ToStringCollection(", "));
            }

        }

        public static T GetService<T>() where T : class
        {
            List<T> foundServices = _services.Where(o => o is T || o.GetType().IsAssignableFrom(typeof(T))).Cast<T>()
                .ToList();
            if (foundServices.Count > 1)
                throw new InvalidOperationException("Multiple services found with " + typeof(T).Name
                                                    + foundServices.ToStringCollection(", "));
            if (foundServices.Count == 1)
                return foundServices[0];
            throw new KeyNotFoundException(typeof(T).FullName);
        }

        public static void RemoveService<T>()
        {
            for (int i = 0; i < _services.Count; i++)
            {
                if (!(_services[i] is T) && !_services[i].GetType().IsAssignableFrom(typeof(T)))
                    continue;
                _services.RemoveAt(i);
                return;
            }
        }
    }

    public interface IA
    {
        void A();
    }

    public interface IB
    {
        void A();
    }

    public class TestA : IA
    {
        public virtual void A() => Console.WriteLine("A");
    }

    public class TestAA : TestA
    {
        public override void A() => Console.WriteLine("AAAAAAAA");
    }

    public class TestB : IB
    {
        public void A() => Console.WriteLine("B");
    }

    public class TestAB : IA, IB
    {
        public void A() => Console.WriteLine("AB");
    }
}