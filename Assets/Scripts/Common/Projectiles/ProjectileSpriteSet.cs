using UnityEngine;

namespace Common.Projectiles
{
    [System.Serializable]
    public class ProjectileSpriteSet
    {
        [SerializeField] private Sprite _playerProjectile;
        [SerializeField] private Sprite _enemyProjectile;

        public Sprite PlayerProjectile => _playerProjectile;
        public Sprite EnemyProjectile => _enemyProjectile;
    }
}