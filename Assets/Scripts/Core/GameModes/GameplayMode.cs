using System;
using Common.Background;
using Common.Player;
using Common.Score;
using Common.Ships;
using Common.Stages;
using Common.UI;
using Core.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Utils;
using UnityEngine;

namespace Core.GameModes
{
    public class GameplayMode : IGameMode, IResetable, IDeactivatable, IDisposable
    {
        public event Action<GameModeArgs> Finished;

        private readonly StagesExecutor _stagesExecutor = new StagesExecutor();
        private readonly Score _score = new Score();
        
        private readonly GameContext _gameContext;
        private readonly Input _input;
        
        private GameplayContext _gameplayContext;
        private ShipConfigsContainer _shipConfigsContainer;
        private IShipGenerator _shipGenerator;
        private IShipsPool _shipsPool;
        private IProjectilePool _projectilesPool;
        private ScoreCounter _scoreCounter;
        private Player _player;
        private Parallax _parallax;

        private bool _isConstructed = false;

        public GameplayMode(GameContext context)
        {
            _gameContext = context;
            _input = context.Resolve<Input>();
        }

        public void Activate(GameModeArgs args)
        {
            if (_isConstructed == false)
            {
                _gameplayContext = _gameContext.Resolve<SceneContext>() as GameplayContext;
                _shipConfigsContainer = _gameplayContext.Resolve<ShipConfigsContainer>();
                _projectilesPool = _gameplayContext.Resolve<IProjectilePool>();
                _shipGenerator = _gameplayContext.Resolve<IShipGenerator>();
                _shipsPool = _gameplayContext.Resolve<IShipsPool>();
                _parallax = _gameplayContext.Resolve<Parallax>();
                
                UIContext uiContext = _gameplayContext.Resolve<UIContext>();
                _scoreCounter = uiContext.Resolve<ScoreCounter>();
                
                SetPlayer();

                _isConstructed = true;
            }

            _stagesExecutor.Initialize(_shipsPool, _shipConfigsContainer);
            _stagesExecutor.Start();
            _parallax.Scroll();
            _scoreCounter.Activate();
            
            SubscribeToEvents();
            AttachInput();
        }

        public void Deactivate()
        {
            _stagesExecutor.Stop();
            _scoreCounter.Deactivate();
            
            DeAttachInput();
            UnsubscribeToEvents();
        }

        public void Dispose()
        {
            Deactivate();
            
            _parallax.Dispose();
            _shipsPool.Dispose();
            _projectilesPool.Dispose();
        }

        public void Reset()
        {
            _gameplayContext = null;

            _isConstructed = false;
        }

        private void AttachInput()
        {
            _input.Gameplay.Move.started += _player.StartMoving;
            _input.Gameplay.Move.canceled += _player.StopMoving;
            
            _input.Gameplay.Enable();
        }

        private void DeAttachInput()
        {
            _input.Gameplay.Disable();
            
            _input.Gameplay.Move.started -= _player.StartMoving;
            _input.Gameplay.Move.canceled -= _player.StopMoving;
        }

        private void SubscribeToEvents()
        {
            _score.Changed += _scoreCounter.UpdateCounter;

            foreach (var ship in _shipsPool.Ships)
            {
                ship.Dead += OnShipDead;
            }
        }

        private void UnsubscribeToEvents()
        {
            _score.Changed -= _scoreCounter.UpdateCounter;
            
            foreach (var ship in _shipsPool.Ships)
            {
                ship.Dead -= OnShipDead;
            }
        }
        
        private void SetPlayer()
        {
            _shipsPool.SetConfig(_shipConfigsContainer.GetConfig(Enums.ShipType.Player));
            
            _player = new Player(_shipsPool.Get());
            _player.SetPosition(new Vector2(Constants.DefaultPlayerXCoordinate, Constants.DefaultPlayerYCoordinate));
            _player.ControllableShip.Show();
            _player.ControllableShip.Activate();
        }
        
        private void OnShipDead(Ship ship) => _score.Add(ship.Score);
    }
}