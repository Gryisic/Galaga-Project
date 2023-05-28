namespace Infrastructure.Utils
{
    public static class Constants
    {
        public const int StartSceneIndex = 0;

        public const float DefaultPlayerXCoordinate = 0f;
        public const float DefaultPlayerYCoordinate = -4f;

        public const float ProjectileLifeTime = 2f;
        public const float ProjectileSpeed = 5f;

        public const float WaveInitializationDelay = 3f;
        public const float WaveChangeDelay = 2f;

        public const string PathToShipPrefab = "Prefabs/Ship";
        public const string PathToShipConfigs = "Configs/Ships";
        public const string PathToWaveConfigs = "Configs/Waves";
        public const string PathToProjectilePrefab = "Prefabs/Projectile";
    }
}
