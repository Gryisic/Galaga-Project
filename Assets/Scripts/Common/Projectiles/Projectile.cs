using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Infrastructure.Utils;
using UnityEngine;

namespace Common.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
    public class Projectile : MonoBehaviour, IDisposable, IDamageDealer, IUnityItem
    {
        public event Action<Projectile> Released; 

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public Enums.ProjectileType ProjectileType { get; private set; }

        private void Awake()
        {
            if (_rigidbody2D == null)
                _rigidbody2D = GetComponent<Rigidbody2D>();

            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.TryGetComponent(out IDamagable damagable))
                damagable.ApplyDamage(this);
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        public void Stop() => _tokenSource.Cancel();

        public void SetSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

        public void SetType(Enums.ProjectileType type) => ProjectileType = type;

        public void Launch(Vector2 from, Vector2 direction) => LaunchAsync(from, direction).Forget();

        private async UniTask LaunchAsync(Vector2 from, Vector2 direction)
        {
            float timer = 0;

            transform.position = from;
            _rigidbody2D.velocity = direction * Constants.ProjectileSpeed;

            while (timer < Constants.ProjectileLifeTime && _tokenSource.IsCancellationRequested == false)
            {
                await UniTask.WaitForFixedUpdate();

                timer += Time.fixedDeltaTime;
            }

            _tokenSource = _tokenSource.CancelAndRefresh();

            _rigidbody2D.velocity = Vector2.zero;
            
            Released?.Invoke(this);
        }
    }
}