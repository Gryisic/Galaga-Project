using System;
using System.Collections.Generic;
using System.Threading;
using Common.Ships;
using Cysharp.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Utils;

namespace Common.Stages
{
    public class Stage : IDisposable
    {
        public event Action Ended;

        private readonly List<Wave> _waves = new List<Wave>();
        private readonly IReadOnlyList<WaveConfig> _configs;
        private readonly ShipConfigsContainer _configsContainer;
        private readonly IShipsPool _shipsPool;

        private CancellationTokenSource _wavesInitializationTokenSource = new CancellationTokenSource();
        
        private int _currentWaveIndex;

        public Stage(IReadOnlyList<WaveConfig> configs, IShipsPool shipsPool, ShipConfigsContainer configsContainer)
        {
            _configs = configs;
            _shipsPool = shipsPool;
            _configsContainer = configsContainer;
        }

        public void Dispose()
        {
            _waves.ForEach(w => w.Dispose());
        }
        
        public void Start()
        {
            SetupWaves(_configs);
        }

        public void Stop()
        {
            _waves.ForEach(wave =>
            {
                wave.Ended -= RemoveWaveFromList;
                wave.ConditionsFulfilled -= ConditionFulfilled;
            });
        }

        private void SetupWaves(IReadOnlyList<WaveConfig> configs)
        {
            foreach (var config in configs)
            {
                Wave wave = new Wave(config);

                _waves.Add(wave);
                
                wave.ConditionsFulfilled += ConditionFulfilled;
                wave.Ended += RemoveWaveFromList;
            }
            
            InitializeNextWaveAsync(_currentWaveIndex).Forget();
        }
        
        private void RemoveWaveFromList(Wave wave)
        {
            wave.Ended -= RemoveWaveFromList;
            
            wave.Stop();
            _waves.Remove(wave);
            
            if (_waves.Count <= 0)
                Ended?.Invoke();
        }

        private void ConditionFulfilled(Wave wave)
        {
            wave.ConditionsFulfilled -= ConditionFulfilled;
            wave.Start();
        }

        private async UniTask InitializeNextWaveAsync(int waveIndex)
        {
            _waves[waveIndex].Initialize(_shipsPool, _configsContainer);

            await UniTask.Delay(TimeSpan.FromSeconds(Constants.WaveInitializationDelay), cancellationToken: _wavesInitializationTokenSource.Token);

            if (_wavesInitializationTokenSource.IsCancellationRequested == false && waveIndex + 1 < _waves.Count)
            {
                waveIndex++;
                await InitializeNextWaveAsync(waveIndex);
            }
        }
    }
}