using System.Collections.Generic;
using System.Linq;
using Common.Factories;
using Common.PrefabsRoots;
using Common.Ships;
using Infrastructure.Interfaces;

namespace Common.Generators
{
    public class ShipsGenerator : UnityItemGenerator<Ship>, IShipGenerator, IShipsPool
    {
        private readonly List<Ship> _ships = new List<Ship>();
        
        private readonly IProjectilePool _projectilePool;
        private ShipConfig _config;

        public IReadOnlyList<Ship> Ships => items.ToList();
        
        public ShipsGenerator(Root root, IProjectilePool projectilePool) : base(root)
        {
            _projectilePool = projectilePool;
            
            factory = new ShipsFactory();
            factory.Load();
        }
        
        public void SetConfig(ShipConfig config) => _config = config;

        protected override Ship ConfiguredItem(Ship item)
        {
            item.UpdateShipData(_config);
            item.Initialize(_projectilePool);

            return item;
        }
    }
}