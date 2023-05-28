using Infrastructure.Utils;

namespace Infrastructure.Interfaces
{
    public interface IDamageDealer
    {
        Enums.ProjectileType ProjectileType { get; }
    }
}