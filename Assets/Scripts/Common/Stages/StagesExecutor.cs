using System;
using System.Threading;
using Common.Ships;
using Cysharp.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Infrastructure.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Stages
{
    public class StagesExecutor : IDisposable
    {
        private CancellationTokenSource _changeWaveTokenSource = new CancellationTokenSource();

        private IShipsPool _shipsPool;
        private ShipConfigsContainer _configsContainer;
        private WaveConfig[] _configs;
        private Stage _currentStage;
        
        public void Initialize(IShipsPool shipsPool, ShipConfigsContainer configsContainer)
        {
            _shipsPool = shipsPool;
            _configsContainer = configsContainer;
            
            _configs = Resources.LoadAll<WaveConfig>(Constants.PathToWaveConfigs);
            CreateStage();
        }

        public void Start()
        {
            _currentStage.Start();
        }

        public void Stop()
        {
            _currentStage.Stop();
            _changeWaveTokenSource.Cancel();
        }

        public void Dispose()
        {
            Stop();
            
            _currentStage.Dispose();
            
            _changeWaveTokenSource.Cancel();
            _changeWaveTokenSource.Dispose();
        }

        private void CreateStage()
        {
            if (_currentStage != null)
            {
                _currentStage.Ended -= ChangeStage;
                _currentStage.Stop();
                _currentStage.Dispose();
            }

            int configsAmount = Random.Range(3, _configs.Length);
            WaveConfig[] randomConfigs = new WaveConfig[configsAmount];

            for (int i = 0; i < configsAmount; i++)
                randomConfigs[i] = _configs[Random.Range(0, _configs.Length)];
            
            _currentStage = new Stage(randomConfigs, _shipsPool, _configsContainer);

            _currentStage.Ended += ChangeStage;
        }

        private void ChangeStage() => ChangeStageAsync().Forget();

        private async UniTask ChangeStageAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Constants.WaveChangeDelay),
                cancellationToken: _changeWaveTokenSource.Token);

            if (_changeWaveTokenSource.IsCancellationRequested == false)
            {
                CreateStage();
                _currentStage.Start();
            }
            
            _changeWaveTokenSource = _changeWaveTokenSource.CancelAndRefresh();
        }
    }
}