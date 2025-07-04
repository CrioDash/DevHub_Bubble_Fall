namespace Db
{
    public interface IBallSettingsDatabase
    {
        public float ShotSpeed { get; }
        public float ShotCooldown { get; }
        public float BallSpeed { get; }
        public int ColumnsCount { get; }
        public float BallSpacing { get; }
        public float SpawnInterval { get; }
        public int StartSpawnСount { get; }
        public float BallDeathSpeed { get; }
        public string KilledBallLayerName { get; }
        public string FlyingBallLayerName { get; }
        public string BallLayerName { get; }
        public float KnockbackDistance { get; }
        public float KnockbackDuration { get; }
    }
}