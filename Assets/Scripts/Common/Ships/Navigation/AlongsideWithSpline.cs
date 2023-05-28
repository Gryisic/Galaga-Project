using System;
using System.Threading;
using Common.Splines;
using Cysharp.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Common.Ships.Navigation
{
    public class AlongsideWithSpline : INavigationStrategy
    {
        public event Action NavigationCompleted;
        
        private readonly Spline _spline;
        private readonly IMovable _movable;

        private CancellationTokenSource _moveTokenSource = new CancellationTokenSource();

        public Vector2 InitialPosition => _spline.Points[0];
        
        public AlongsideWithSpline(Spline spline, IMovable movable)
        {
            _spline = spline;
            _movable = movable;
        }
        
        public void Dispose()
        {
            _moveTokenSource.Cancel();
            _moveTokenSource.Dispose();
        }
        
        public void StartMoving() => MoveAsync().Forget();

        public void StopMoving() 
        {
            _movable.StopMoving();
            _moveTokenSource.Cancel();
        }

        private async UniTask MoveAsync()
        {
            foreach (var point in _spline.Points)
            {
                if (_moveTokenSource.IsCancellationRequested)
                {
                    _movable.StopMoving();
                    _moveTokenSource = _moveTokenSource.CancelAndRefresh();
                    
                    break;
                }

                Vector2 movablePosition = _movable.Transform.position;
                Vector2 direction = (point - movablePosition).normalized;
                float distance = (point - movablePosition).magnitude;
                float movingTime = distance / _movable.Speed;
                
                _movable.StartMoving(direction);

                await UniTask.Delay(TimeSpan.FromSeconds(movingTime), cancellationToken: _moveTokenSource.Token);
            }
            
            _movable.StopMoving();
            NavigationCompleted?.Invoke();
        }
    }
}