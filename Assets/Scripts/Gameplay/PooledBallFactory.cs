using System;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class PooledBallFactory : IBallFactory, IDisposable
    {
        private readonly Ball.Pool _pool;
        
        [Inject]
        public PooledBallFactory(Ball.Pool pool) {
            _pool = pool;
        }

        public Ball Create(Vector3 position, int colorId) {
            return _pool.Spawn(position, colorId);
        }

        public void Dispose() {
            _pool.Clear();
        }
    }

}