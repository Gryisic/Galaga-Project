using System;
using System.Collections.Generic;
using Common.UI;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.Contexts
{
    public class GameContext : MonoBehaviour, IDisposable 
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private SceneTransition _sceneTransition;

        private SceneSwitcher _sceneSwitcher;

        private Dictionary<Type, object> _registeredTypes;

        public void Construct()
        {
            _registeredTypes = new Dictionary<Type, object>();
            _sceneSwitcher = new SceneSwitcher(_sceneTransition);

            RegisterInstance(_sceneSwitcher);
            RegisterInstance(_mixer);
            RegisterInstance(_sceneTransition);
            RegisterInstance(GetInput());

            _sceneSwitcher.SceneChanged += RegisterSceneContext;
        }

        public void Dispose()
        {
            _sceneSwitcher.SceneChanged -= RegisterSceneContext;
        }

        public T Resolve<T>() => (T)_registeredTypes[typeof(T)];

        private void RegisterSceneContext(SceneContext context)
        {
            if (_registeredTypes.ContainsKey(typeof(SceneContext)))
                _registeredTypes.Remove(typeof(SceneContext));

            context.Construct();
            RegisterInstance(context);
        }

        private void RegisterInstance<T>(T instance) => _registeredTypes.Add(typeof(T), instance);

        private Input GetInput() => new Input();
    }
}