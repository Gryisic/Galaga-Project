using Common.Projectiles;
using Infrastructure.Utils;

namespace Infrastructure.Interfaces
{
    public interface IProjectilePool : IItemsPool<Projectile>
    {
        void SetType(Enums.ProjectileType type);
    }
}