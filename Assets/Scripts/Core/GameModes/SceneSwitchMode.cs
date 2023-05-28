using System;
using Core.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Utils;

namespace Core.GameModes
{
    public class SceneSwitchMode : IGameMode, IDeactivatable
    {
        public event Action<GameModeArgs> Finished;

        private readonly SceneSwitcher _sceneSwitcher;
        
        private Enums.GameModeType _newMode;

        public SceneSwitchMode(GameContext gameContext) 
        {
            _sceneSwitcher = gameContext.Resolve<SceneSwitcher>();
        }
        
        public void Activate(GameModeArgs args)
        {
            if (args.NewScene == Enums.SceneType.Default)
                throw new ArgumentException($"New Scene isn't setted");
            
            _newMode = args.CurrentMode;

            _sceneSwitcher.ChangeScene(args.NewScene);
            
            _sceneSwitcher.SceneChanged += SceneChanged;
        }

        public void Deactivate()
        {
            _sceneSwitcher.SceneChanged -= SceneChanged;
        }
        
        private void SceneChanged(SceneContext sceneContext) => Finished?.Invoke(new GameModeArgs(_newMode));
    }
}