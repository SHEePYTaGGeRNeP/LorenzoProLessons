using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Helpers.Classes
{
    public static class ServiceLocator
    {
        private static readonly List<object> _services = new List<object>();

        public static void Clear()
        {
            for (int i = _services.Count - 1; i >= 0; i--)
                _services.RemoveAt(i);
        }

        public static void AddService<T>(T t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            foreach (object o in _services)
            {
                if (o is T || o.GetType().IsAssignableFrom(typeof(T)))
                    throw new ServiceAlreadyExistsException(
                        $"Cannot add: [{typeof(T).FullName} {t}], because there already is an {o.GetType().FullName}");
            }
            _services.Add(t);
        }

        // IA  - TestA
        // IB  - TestB
        // IA/IB - TestAB
        
        // <IB> TestAB 
        public static void AddOrReplaceService<T>(T serviceToAdd) where T:  class
        {
            if (serviceToAdd == null)
                throw new ArgumentNullException(nameof(serviceToAdd));
            List<T> foundServices = new List<T>();
            int index = -1;
            for (int i = 0; i < _services.Count; i++)
            {
                if (!(_services[i] is T) 
                    && !_services[i].GetType().IsAssignableFrom(typeof(T))
                    && !_services[i].GetType().IsInstanceOfType(serviceToAdd)
                    && !serviceToAdd.GetType().IsInstanceOfType(_services[i]))
                    continue;
                foundServices.Add((T) _services[i]);
                if (index == -1)
                    index = i;
            }
            switch (foundServices.Count)
            {
                case 0:
                    AddService(serviceToAdd);
                    break;
                case 1:
                    _services[index] = serviceToAdd;
                    return;
                default:
                    throw new MultipleServicesAlreadyExistsException("Multiple services found with " + typeof(T).Name
                                                        + foundServices.ToStringCollection(", "));
            }
        }

        public static T GetService<T>()
        {
            T[] foundServices = _services.Where(o => o is T || o.GetType().IsAssignableFrom(typeof(T))).Cast<T>().ToArray();
            if (foundServices.Length == 1)
                return foundServices[0];
            if (foundServices.Length > 1)
                throw new InvalidOperationException("Multiple services found with " + typeof(T).Name
                                                    + foundServices.ToStringCollection(", "));
            throw new KeyNotFoundException(typeof(T).FullName);
        }

        public static void RemoveService<T>()
        {
            for (int i = 0; i < _services.Count; i++)
            {
                if (!(_services[i] is T))
                    continue;
                _services.RemoveAt(i);
                return;
            }
        }
        
        public class ServiceAlreadyExistsException: Exception
        {
            public ServiceAlreadyExistsException(string message) : base(message)
            {

            }
        }
        public class MultipleServicesAlreadyExistsException: Exception
        {
            public MultipleServicesAlreadyExistsException(string message) : base(message)
            {

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
        public virtual void A() => Debug.Log("A");
    }

    public class TestAA : TestA
    {
        public override void A() => Debug.Log("AAAAAAAA");
    }

    public class TestB : IB
    {
        public void A() => Debug.Log("B");
    }

    public class TestAB : IA, IB
    {
        public void A() => Debug.Log("AB");
    }
}