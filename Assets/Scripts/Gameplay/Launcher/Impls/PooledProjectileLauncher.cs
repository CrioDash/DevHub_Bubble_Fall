using Db;
using Gameplay.Launcher.Interfaces;
using UnityEngine;

namespace Gameplay.Launcher.Impls
{
    public class PooledProjectileLauncher : IProjectileLauncher
    {
        private readonly IBallFactory _factory;
        private readonly IBallSettingsDatabase _ballSettingsDatabase;

        public PooledProjectileLauncher(IBallFactory factory, IBallSettingsDatabase ballSettingsDatabase)
        {
            _factory = factory;
            _ballSettingsDatabase = ballSettingsDatabase;
        }

        public void Launch(int colorId, Vector3 origin, Vector3 direction)
        {
            var ball = _factory.Create(origin, colorId);
            
            ball.gameObject.layer = LayerMask.NameToLayer(_ballSettingsDatabase.FlyingBallLayerName);
            
            var rb = ball.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.linearVelocity = direction * _ballSettingsDatabase.ShotSpeed;
        }
    }
}