using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Extensions;
using UnityEngine;

namespace Common.Stages.Conditions
{
    public class Delay : NextWaveCondition
    {
        public override event Action Fulfilled;

        private CancellationTokenSource _delayTokenSource = new CancellationTokenSource();

        private float _delay;
        
        public Delay(float delay)
        {
            _delay = delay;
        }
        
        public override void StartChecking()
        {
            _delayTokenSource = _delayTokenSource.CancelAndRefresh();
            
            CheckingAsync().Forget();
        }

        public override void StopChecking() => _delayTokenSource.Cancel();

        private async UniTask CheckingAsync()
        {
            while (_delayTokenSource.IsCancellationRequested == false)
            {
                await UniTask.WaitForFixedUpdate(cancellationToken: _delayTokenSource.Token);

                _delay -= Time.fixedDeltaTime;
                
                if (_delay <= 0)
                    Fulfilled?.Invoke();
            }
            
            _delayTokenSource.Cancel();
            _delayTokenSource.Dispose();
        }
    }
}