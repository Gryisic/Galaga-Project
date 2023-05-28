using System.Collections.Generic;
using Common.Ships;

namespace Infrastructure.Interfaces
{
    public interface IShipsPool : IItemsPool<Ship>
    {
        IReadOnlyList<Ship> Ships { get; }

        void SetConfig(ShipConfig config);
    }
}