using System;
using Common.Projectiles;
using Common.Ships;
using Core.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Utils;

namespace Core.GameModes
{
    public class GameplayInitMode : IGameMode
    {
        public event Action<GameModeArgs> Finished;

        private readonly GameContext _gameContext;
        
        private GameplayContext _gameplayContext;

        public GameplayInitMode(GameContext gameContext)
        {
            _gameContext = gameContext;
        }
        
        public void Activate(GameModeArgs args)
        {
            _gameplayContext = _gameContext.Resolve<SceneContext>() as GameplayContext;

            ShipConfigsContainer shipConfigsContainer = _gameplayContext.Resolve<ShipConfigsContainer>();
            shipConfigsContainer.Initialize();
            
            CreateShips();
            CreateProjectiles();
            
            Finished?.Invoke(new GameModeArgs(Enums.GameModeType.Gameplay));
        }

        private void CreateShips()
        {
            IShipGenerator shipsGenerator = _gameplayContext.Resolve<IShipGenerator>();

            shipsGenerator.CreateItems(20);
        }
        
        private void CreateProjectiles()
        {
            IProjectileGenerator projectileGenerator = _gameplayContext.Resolve<IProjectileGenerator>();
            ProjectileSpriteSet projectileSpriteSet = _gameplayContext.Resolve<ProjectileSpriteSet>();

            projectileGenerator.CreateItems(40);
            projectileGenerator.SetSprites(projectileSpriteSet.PlayerProjectile, projectileSpriteSet.EnemyProjectile);
        }
    }
}