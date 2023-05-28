using System;
using System.Collections.Generic;
using System.Threading;
using Common.Ships;
using Common.Ships.Navigation;
using Common.Stages.Conditions;
using Cysharp.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Utils;
using Random = UnityEngine.Random;

namespace Common.Stages
{
    public class Wave : IDisposable
    {
        public event Action<Wave> Ended;
        public event Action<Wave> ConditionsFulfilled; 

        private readonly CancellationTokenSource _startTokenSource = new CancellationTokenSource();
        private readonly List<Ship> _ships = new List<Ship>();
        private readonly WaveConfig _config;

        private IShipsPool _shipsPool;
        private ShipConfigsContainer _shipConfigsContainer;
        private NextWaveCondition _condition;
        
        private List<NavigationDataProvider> _navigationDataProviders;
        private bool _isInitialized;

        public Wave(WaveConfig config)
        {
            _config = config;
        }

        public void Initialize(IShipsPool shipsPool, ShipConfigsContainer configsContainer)
        {
            if (_isInitialized)
                return;

            _shipsPool = shipsPool;
            _shipConfigsContainer = configsContainer;
            
            _condition = DefineCondition(_config.ConditionType);
            _condition.StartChecking();

            _navigationDataProviders = _config.NavigationProviders;

            GetShipsFromPool();
            SubscribeToEvents();

            _isInitialized = true;
        }
        
        public void Dispose()
        {
            Stop();
            
            _startTokenSource.Cancel();
            _startTokenSource.Dispose();
        }

        public void Start()
        {
            StartAsync().Forget();
        }

        public void Stop()
        {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _condition.Fulfilled += ConditionFulfilled;

            foreach (var ship in _ships)
            {
                ship.Navigation.RequestNavigationProvider += GetNavigationProvider;
                ship.Navigation.Completed += ReturnShipToPool;
                ship.Dead += ReturnShipToPool;
            }
        }

        private void UnsubscribeToEvents()
        {
            _condition.Fulfilled -= ConditionFulfilled;
            
            foreach (var ship in _ships)
            {
                ship.Navigation.RequestNavigationProvider -= GetNavigationProvider;
                ship.Navigation.Completed -= ReturnShipToPool;
                ship.Dead -= ReturnShipToPool;
            }
        }
        
        private void ConditionFulfilled()
        {
            _condition.Fulfilled -= ConditionFulfilled;
            
            _condition.StopChecking();
            
            ConditionsFulfilled?.Invoke(this);
        }

        private void GetShipsFromPool()
        {
            for (int i = 0; i < _config.ShipsAmount; i++)
            {
                Enums.ShipType type = _config.Ships[Random.Range(0, _config.Ships.Count)];
                ShipConfig config = _shipConfigsContainer.GetConfig(type);

                _shipsPool.SetConfig(config);

                Ship ship = _shipsPool.Get();

                _ships.Add(ship);
            }
        }
        
        private void ReturnShipToPool(Ship ship)
        {
            ship.Navigation.Stop();
            ship.Deactivate();
            ship.Hide();
            
            _ships.Remove(ship);
            _shipsPool.Return(ship);

            if (_ships.Count <= 0)
                Ended?.Invoke(this);
        }
        
        private void ReturnShipToPool(IMovable movable)
        {
            Ship ship = movable as Ship;
            
            ReturnShipToPool(ship);
        }

        private NextWaveCondition DefineCondition(Enums.NextWaveConditionType type)
        {
            switch (type)
            {
                case Enums.NextWaveConditionType.Delay:
                    return new Delay(_config.Delay);

                case Enums.NextWaveConditionType.AllEnemiesDead:
                    break;
            }

            return null;
        }
        
        private NavigationDataProvider GetNavigationProvider(int index)
        {
            if (index >= _navigationDataProviders.Count)
                return null;
            
            NavigationDataProvider dataProvider = _navigationDataProviders[index];

            return dataProvider;
        }

        private async UniTask StartAsync()
        {
            List<Ship> ships = new List<Ship>(_ships); 

            foreach (var ship in ships)
            {
                ship.Navigation.Start();
                ship.Show();
                ship.Activate();

                await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _startTokenSource.Token);
            }
        }
    }
}