using System;
using Core.Contexts;
using Infrastructure.Interfaces;
using static Infrastructure.Utils.Enums;

namespace Core.GameModes
{
    public class GameInitMode : IGameMode, IDeactivatable
    {
        public event Action<GameModeArgs> Finished;

        private SceneType _initialScene;

        public GameInitMode(GameContext context, SceneType initialScene)
        {
            _initialScene = initialScene;
        }

        public void Activate(GameModeArgs args)
        {
            Finished?.Invoke(new GameModeArgs(GameModeType.SceneSwitch, GameModeType.GameplayInitMode, _initialScene));
        }

        public void Deactivate() { }
    }
}