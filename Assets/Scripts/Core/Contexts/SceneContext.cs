using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Contexts
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private UIContext _uiContext;

        private Dictionary<Type, object> _registeredTypes;

        public IReadOnlyDictionary<Type, object> RegisteredTypes => _registeredTypes;

        public virtual void Construct()
        {
            _registeredTypes = new Dictionary<Type, object>();

            RegisterInstance(_uiContext);
        }

        public T Resolve<T>() => (T)_registeredTypes[typeof(T)];

        protected void RegisterInstance<T>(T instance) => _registeredTypes.Add(typeof(T), instance);

        protected void RegisterInstance<T1, T2>(object instance)
        {
            _registeredTypes.Add(typeof(T1), instance);
            _registeredTypes.Add(typeof(T2), instance);
        }
    }
}