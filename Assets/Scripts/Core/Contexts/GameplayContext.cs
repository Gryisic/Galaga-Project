using Common.Background;
using Common.Generators;
using Common.PrefabsRoots;
using Common.Projectiles;
using Common.Ships;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Core.Contexts
{
    public class GameplayContext : SceneContext
    {
        [SerializeField] private ShipsRoot _shipsRoot;
        
        [Space] [SerializeField] private ProjectileRoot _projectileRoot;
        [SerializeField] private ProjectileSpriteSet _projectileSpriteSet;

        [Space] [SerializeField] private Parallax _parallax;

        public override void Construct()
        {
            base.Construct();
            
            RegisterInstance(_projectileSpriteSet);
            RegisterInstance(_parallax);
            
            RegisterInstance(GetShipConfigsContainer());
            RegisterInstance<IProjectileGenerator, IProjectilePool>(GetProjectilesGenerator());
            RegisterInstance<IShipGenerator, IShipsPool>(GetShipGenerator());
        }

        private ShipsGenerator GetShipGenerator()
        {
            IProjectilePool pool = Resolve<IProjectilePool>();
            
            return new ShipsGenerator(_shipsRoot, pool);
        }

        private ProjectilesGenerator GetProjectilesGenerator() => new ProjectilesGenerator(_projectileRoot);

        private ShipConfigsContainer GetShipConfigsContainer() => new ShipConfigsContainer();
    }
}