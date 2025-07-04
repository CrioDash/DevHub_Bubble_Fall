using UnityEngine;

namespace Db.Impls
{
    [CreateAssetMenu(menuName = "Ball/BallSettingsDatabase", fileName = "BallSettingsDatabase")]
    public class BallSettingsDatabase : ScriptableObject, IBallSettingsDatabase
    {
        [SerializeField] private float shotSpeed;
        [SerializeField] private float shotCooldown;
        [SerializeField] private float ballSpeed;
        [SerializeField] private int columnsCount;
        [SerializeField] private float ballSpacing;
        [SerializeField] private float spawnInterval;
        [SerializeField] private int startSpawnСount;
        [SerializeField] private float ballDeathSpeed;
        [SerializeField] private string killedBallLayerName;
        [SerializeField] private string flyingBallLayerName;
        [SerializeField] private string ballLayerName;
        [SerializeField] private float knockbackDistance;
        [SerializeField] private float knockbackDuration;

        public float ShotSpeed => shotSpeed;
        public float ShotCooldown => shotCooldown;
        public float BallSpeed => ballSpeed;
        public int ColumnsCount => columnsCount;
        public float BallSpacing => ballSpacing;
        public float SpawnInterval => spawnInterval;
        public int StartSpawnСount => startSpawnСount;
        public float BallDeathSpeed => ballDeathSpeed;
        public string KilledBallLayerName => killedBallLayerName;
        public string FlyingBallLayerName => flyingBallLayerName;
        public string BallLayerName => ballLayerName;
        public float KnockbackDistance => knockbackDistance;
        public float KnockbackDuration => knockbackDuration;
    }
}