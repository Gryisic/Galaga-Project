using Cysharp.Threading.Tasks;
using Infrastructure.Utils;
using System;
using Common.UI;
using Core.Contexts;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Infrastructure.Utils.Enums;

namespace Core
{
    public class SceneSwitcher
    {
        public event Action SceneChangeInitiated;
        public event Action<SceneContext> SceneChanged;

        private readonly SceneTransition _sceneTransition;
        
        private int _currentSceneIndex;
        private int _nextSceneIndex;

        public SceneSwitcher(SceneTransition sceneTransition)
        {
            _sceneTransition = sceneTransition;
        }
        
        public void ReloadCurrentScene()
        {
            ReloadSceneAsync().Forget();
        }

        public void ChangeScene(SceneType sceneType)
        {
            ChangeSceneAsync(sceneType).Forget();
        }

        public async UniTask ChangeSceneAsync(SceneType sceneType)
        {
            SceneChangeInitiated?.Invoke();

            _nextSceneIndex = (int) sceneType;

            await ToggleSceneTransitionAsync(true);
            
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(_nextSceneIndex, LoadSceneMode.Additive);

            await LoadSceneAsync(loadScene);
            
            ToggleSceneTransitionAsync(false).Forget();

            if (_currentSceneIndex != Constants.StartSceneIndex)
                loadScene.completed += UnloadSceneAsync;

            loadScene.completed -= OnSceneLoadCompleted;
            loadScene.completed -= UnloadSceneAsync;

            _currentSceneIndex = (int) sceneType;
        }

        private async UniTask ReloadSceneAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            SceneChangeInitiated?.Invoke();

            AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(_currentSceneIndex);

            await UniTask.WaitUntil(() => unloadScene.isDone);

            AsyncOperation loadScene = SceneManager.LoadSceneAsync(_currentSceneIndex, LoadSceneMode.Additive);

            await LoadSceneAsync(loadScene);

            loadScene.completed -= OnSceneLoadCompleted;
        }

        private async UniTask LoadSceneAsync(AsyncOperation loadScene)
        {
            loadScene.allowSceneActivation = false;

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            loadScene.completed += OnSceneLoadCompleted;

            UniTask secondDelayTask = UniTask.Delay(TimeSpan.FromSeconds(0f));
            UniTask loadSceneTask = UniTask.WaitUntil(() => loadScene.progress == 0.9f);

            await UniTask.WhenAll(secondDelayTask, loadSceneTask);

            loadScene.allowSceneActivation = true;

            await UniTask.WaitUntil(() => loadScene.isDone);
        }

        private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;

            SceneChanged?.Invoke(GetContext(SceneManager.GetSceneByBuildIndex(_nextSceneIndex)));
        }

        private void OnSceneLoadCompleted(AsyncOperation operation)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(_nextSceneIndex);

            SceneManager.SetActiveScene(scene);
        }
        
        private async void UnloadSceneAsync(AsyncOperation obj) => 
            await SceneManager.UnloadSceneAsync(_currentSceneIndex);

        private async UniTask ToggleSceneTransitionAsync(bool activate)
        {
            switch (activate)
            {
                case true:
                    await _sceneTransition.ActivateAsync();
                    break;
                
                case false:
                    await _sceneTransition.DeactivateAsync();
                    break;
            }
        }

        private SceneContext GetContext(Scene scene)
        {
            var allObjects = scene.GetRootGameObjects();

            foreach (var obj in allObjects)
            {
                if (obj.TryGetComponent(out SceneContext context))
                    return context;
            }

            throw new MissingComponentException($"{scene.name} doesn't contains a Scene Context");
        }
    }
}