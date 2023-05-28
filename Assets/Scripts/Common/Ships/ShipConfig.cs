using Infrastructure.Utils;
using UnityEngine;

namespace Common.Ships
{
    [CreateAssetMenu(menuName = "Gameplay / Configs / Ship", fileName = "Ship")]
    public class ShipConfig : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _health = 1;
        [SerializeField] private int _score;

        [SerializeField] private Enums.ProjectileType _projectileType;
        [SerializeField] private Enums.ProjectileDirection _projectileDirection;
        [Range(1, 6)] [SerializeField] private float _shootCooldown;
        [SerializeField] private AudioClip[] _shootSounds;
        
        public Sprite Sprite => _sprite;
        public int Health => _health;
        public int Score => _score;

        public Enums.ProjectileType ProjectileType => _projectileType;
        public Enums.ProjectileDirection ProjectileDirection => _projectileDirection;
        public float ShootCooldown => _shootCooldown;
        public AudioClip[] ShootSounds => _shootSounds;
    }
}