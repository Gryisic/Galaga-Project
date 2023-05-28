using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface IProjectileGenerator
    {
        void CreateItems(int amount);
        void SetSprites(Sprite playerSprite, Sprite enemySprite);
    }
}