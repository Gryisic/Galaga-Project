using System;
using System.Threading;
using Common.Projectiles;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Infrastructure.Extensions;
using Infrastructure.Utils;
using UnityEngine;

namespace Common.Ships
{
    public class Behaviour : IDisposable
    {
        public event Func<Enums.ProjectileType, Projectile> RequestProjectile;
        public event Action<Projectile> ReleaseProjectile;
        public event Action ProjectileLaunched;

        private readonly Transform _origin;

        private CancellationTokenSource _shootTokenSource = new CancellationTokenSource();
        private CancellationTokenSource _highlightingSource = new CancellationTokenSource();
        
        private ShipConfig _config;
        private SpriteRenderer _renderer;

        public Enums.ProjectileType ProjectileType => _config.ProjectileType;

        public Behaviour(Transform origin, SpriteRenderer renderer)
        {
            _origin = origin;
            _renderer = renderer;
        }
        
        public void Dispose()
        {
            _shootTokenSource.Cancel();
            _shootTokenSource.Dispose();
            
            _highlightingSource.Cancel();
            _highlightingSource.Dispose();
        }
        

        public void UpdateConfig(ShipConfig config) => _config = config;

        public void Activate()
        {
            if (_config == null) 
                throw new NullReferenceException($"{_origin.name}'s {_origin.GetInstanceID()} config isn't setted");
            
            ShootAsync().Forget();
        }

        public void Deactivate()
        {
            _shootTokenSource.Cancel();

            _config = null;
        }
        
        public void DamageTaken()
        {
            DamageTakenHighlightingAsync().Forget();
        }

        private void RaiseProjectileReleasedEvent(Projectile projectile)
        {
            projectile.Released -= RaiseProjectileReleasedEvent;
            
            ReleaseProjectile?.Invoke(projectile);
        }
        
        private async UniTask ShootAsync()
        {
            Vector2 projectileDirection =
                _config.ProjectileDirection == Enums.ProjectileDirection.Down ? Vector2.down : Vector2.up;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_config.ShootCooldown));
            
            while (_shootTokenSource.IsCancellationRequested == false)
            {
                Projectile projectile = RequestProjectile?.Invoke(_config.ProjectileType);
                projectile.Released += RaiseProjectileReleasedEvent;

                projectile.Launch(_origin.position, projectileDirection);

                ProjectileLaunched?.Invoke();

                await UniTask.Delay(TimeSpan.FromSeconds(_config.ShootCooldown));
            }

            _shootTokenSource = _shootTokenSource.CancelAndRefresh();
        }

        private async UniTask DamageTakenHighlightingAsync()
        {
            UniTask highlightingSequence = DOTween.Sequence().
                                                    Append(_renderer.DOFade(0f, 0.2f)).
                                                    Append(_renderer.DOFade(1f, 0.2f)).
                                                    SetLoops(2, LoopType.Yoyo).
                                                    ToUniTask(cancellationToken: _highlightingSource.Token);

            await highlightingSequence;
        }
    }
}