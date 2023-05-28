using Core.Contexts;
using Core.GameModes;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using UnityEngine;
using static Infrastructure.Utils.Enums;

namespace Core
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;
        [SerializeField] private SceneType _initialScene;

        private IGameMode _currentMode;
        private IGameMode[] _gameModes;

        private void Awake()
        {
            _gameContext.Construct();

            _gameModes = new IGameMode[]
            {
                new GameInitMode(_gameContext, _initialScene),
                new SceneSwitchMode(_gameContext),
                new GameplayInitMode(_gameContext),
                new GameplayMode(_gameContext),
            };
        }

        private void Start()
        {
            ChangeActiveGameMode(new GameModeArgs(GameModeType.GameInit));
        }

        private void OnEnable()
        {
            foreach (var mode in _gameModes)
            {
                mode.Finished += OnGameModeFinished;
            }
        }

        private void OnDisable()
        {
            foreach (var mode in _gameModes)
            {
                mode.Finished -= OnGameModeFinished;
            }
        }

        private void OnDestroy()
        {
            foreach (var mode in _gameModes)
            {
                mode.Dispose();
            }

            _gameContext.Dispose();
        }

        private void OnGameModeFinished(GameModeArgs args)
        {
            ChangeActiveGameMode(args);
        }

        private void ChangeActiveGameMode(GameModeArgs args)
        {
            _currentMode?.Deactivate();
            _currentMode?.Reset();
            _currentMode = _gameModes[(int)args.NewMode];
            _currentMode.Activate(args);
        }
    }
}