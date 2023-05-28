namespace Infrastructure.Utils
{
    public static class Enums 
    {
        public enum GameModeType 
        {
            GameInit,
            SceneSwitch,
            GameplayInitMode,
            Gameplay
        }

        public enum SceneType 
        {
            Default,
            Gameplay
        }

        public enum ShipType
        {
            Player,
            Bee
        }

        public enum ProjectileType
        {
            Player,
            Enemy
        }
        
        public enum ProjectileDirection
        {
            Down,
            Up
        }

        public enum NextWaveConditionType
        {
            Delay,
            AllEnemiesDead
        }

        public enum NavigationType
        {
            AlongsideWithSpline,
        }
    }
}
