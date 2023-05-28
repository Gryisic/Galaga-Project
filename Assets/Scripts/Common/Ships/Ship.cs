using System;
using Common.Projectiles;
using Infrastructure.Interfaces;
using Infrastructure.Utils;
using UnityEngine;

namespace Common.Ships
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(AudioSource))]
    public class Ship : MonoBehaviour, IDisposable, IDamagable, IMovable, INavigationProvider, IUnityItem
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private AudioSource _audioSource;
        
        public event Action<Ship> Dead;
        public event Action DamageTaken;

        private Behaviour _behaviour;
        private Audio _audio;
        private IProjectilePool _projectilePool;

        private bool _isActivated;
        private int _health;

        public float Speed { get; } = 5;

        public Navigation.Navigation Navigation { get; private set; }
        public int Score { get; private set; }
        public Transform Transform => transform;
        public ShipConfig Config { get; private set; }

        private void Awake()
        {
            if (_rigidbody2D == null)
                _rigidbody2D = GetComponent<Rigidbody2D>();

            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();

            _behaviour = new Behaviour(transform, _spriteRenderer);
            _audio = new Audio(_audioSource);
            Navigation = new Navigation.Navigation(this);
        }

        public void Initialize(IProjectilePool projectilesPool)
        {
            _projectilePool = projectilesPool;
        }
        
        public void Dispose()
        {
            if (_isActivated)
                Deactivate();
            
            _behaviour.Dispose();
            Navigation.Dispose();
        }
        
        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        public void Activate()
        {
            _isActivated = true;
            
            SubscribeToEvents();

            _behaviour.Activate();
        }

        public void Deactivate() 
        {
            _behaviour.Deactivate();
            
            UnsubscribeToEvents();

            _isActivated = false;
        }
        
        public void SetPosition(Vector2 position) => transform.position = position;

        public void UpdateShipData(ShipConfig config)
        {
            _audio.UpdateClips(config.ShootSounds);
            _spriteRenderer.sprite = config.Sprite;
            _health = config.Health;
            Score = config.Score;

            Config = config;

            _behaviour.UpdateConfig(config);
        }

        public void ApplyDamage(IDamageDealer dealer)
        {
            if (dealer.ProjectileType == _behaviour.ProjectileType)
                return;

            _health--;

            if (_health <= 0)
            {
                Dead?.Invoke(this);

                Debug.Log(Config.Score);
                
                Deactivate();
                
                return;
            }
            
            DamageTaken?.Invoke();
        }
        
        public void StartMoving(Vector2 direction) => _rigidbody2D.velocity = direction * Speed;

        public void StopMoving() => _rigidbody2D.velocity = Vector2.zero;

        private void SubscribeToEvents()
        {
            _behaviour.RequestProjectile += ProjectileRequested;
            _behaviour.ReleaseProjectile += ProjectileReleased;
            _behaviour.ProjectileLaunched += _audio.PlayRandom;
            
            DamageTaken += _behaviour.DamageTaken;
        }

        private void UnsubscribeToEvents()
        {
            _behaviour.RequestProjectile -= ProjectileRequested;
            _behaviour.ReleaseProjectile -= ProjectileReleased;
            _behaviour.ProjectileLaunched -= _audio.PlayRandom;
            
            DamageTaken -= _behaviour.DamageTaken;
        }
        
        private Projectile ProjectileRequested(Enums.ProjectileType projectileType)
        {
            _projectilePool.SetType(projectileType);
            
            Projectile projectile = _projectilePool.Get();
            projectile.Show();
            
            return projectile;
        }
        
        private void ProjectileReleased(Projectile projectile)
        {
            projectile.Hide();
            projectile.Stop();
            
            _projectilePool.Return(projectile);
        }
    }
}