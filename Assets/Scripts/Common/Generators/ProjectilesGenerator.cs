using Common.Factories;
using Common.PrefabsRoots;
using Common.Projectiles;
using Infrastructure.Interfaces;
using Infrastructure.Utils;
using UnityEngine;

namespace Common.Generators
{
    public class ProjectilesGenerator : UnityItemGenerator<Projectile>, IProjectileGenerator, IProjectilePool
    {
        private Sprite _playerSprite;
        private Sprite _enemySprite;

        private Enums.ProjectileType _type;
        
        public ProjectilesGenerator(Root root) : base(root)
        {
            factory = new ProjectileFactory();
            factory.Load();
        }

        public void SetSprites(Sprite playerSprite, Sprite enemySprite)
        {
            _playerSprite = playerSprite;
            _enemySprite = enemySprite;
        }

        public void SetType(Enums.ProjectileType type) => _type = type;

        protected override Projectile ConfiguredItem(Projectile item)
        {
            switch (_type)
            {
                case Enums.ProjectileType.Player:
                    item.SetSprite(_playerSprite);
                    break;
                
                case Enums.ProjectileType.Enemy:
                    item.SetSprite(_enemySprite);
                    break;
            }

            item.SetType(_type);
            return item;
        }
    }
}