using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Helpers.Components
{
    public class Toolbox : Singleton<Toolbox>
    {
        // Used to track any global components added at runtime.
        private Dictionary<string, object> _components = new Dictionary<string, object>();


        // Prevent constructor use.
        protected Toolbox() { }


        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }


        // Define all required global components here. These are hard-codded components
        // that will always be added. Unlike the optional components added at runtime.


        // The methods below allow us to add global components at runtime.
        // TODO: Convert from string IDs to component types.
        public T CreateToolboxComponent<T>(string componentID) where T : Component
        {
            if (_components.ContainsKey(componentID))
                throw new InvalidOperationException($"[Toolbox] componentID \"{componentID}\" already exist! Returning that.");
            T newComponent = gameObject.AddComponent<T>();
            _components.Add(componentID, newComponent);
            return newComponent;
        }

        public void AddToToolbox(string componentID, object obj)
        {
            if (this._components.ContainsKey(componentID))
                throw new InvalidOperationException($"[Toolbox] Global component ID \"{componentID}\" already exist!");
            this._components.Add(componentID, obj);
        }


        public void DestroyToolboxComponent(string componentID)
        {
            if (_components.TryGetValue(componentID, out object obj))
            {
                if (obj is Component component)
                    Destroy(component);
                _components.Remove(componentID);
            }
            else
                throw new InvalidOperationException($"[Toolbox] Trying to remove nonexistent component ID \"{componentID}\"! Typo?");
        }

        public T GetToolboxComponent<T>() => this.GetToolboxComponent<T>(nameof(T));
        public T GetToolboxComponent<T>(string componentID)
        {
            if (_components.TryGetValue(componentID, out object obj))
            {
                if (obj is T gen)
                    return gen;
                throw new InvalidOperationException($"[Toolbox] Global component ID \"{componentID}\" is {obj.GetType().Name} and not {typeof(T).Name}");
            }
            throw new InvalidOperationException($"[Toolbox] Global component ID \"{componentID}\" doesn't exist! Typo?");
        }
    }
}
