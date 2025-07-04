using System;
using Core;
using Db;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class Ball : MonoBehaviour, IPoolable<Vector3, int>
    {
        public Color BallColor { get; private set; }
        public int ColorId { get; private set; }

        private MeshRenderer _renderer;
        private Rigidbody _rigidbody;
        private Pool _pool;
        private ChainManager _chainManager;
        private IBallSettingsDatabase _ballSettingsDatabase;

        [Inject]
        private void Construct(Pool pool,
            ChainManager chainManager,
            IBallSettingsDatabase ballSettingsDatabase)
        {
            _pool = pool;
            _chainManager = chainManager;
            _ballSettingsDatabase = ballSettingsDatabase;
        }

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnSpawned(Vector3 position, int colorId)
        {
            transform.position = position;
            
            ColorId = colorId;
            
            BallColor = BallColorPalette.GetColorById(colorId);
            _renderer.material.color = BallColor;

            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            
            gameObject.layer = LayerMask.NameToLayer(_ballSettingsDatabase.BallLayerName);
            gameObject.SetActive(true);
        }

        public void OnDespawned()
        {
            gameObject.SetActive(false);
        }

        public void Despawn()
        {
            _pool.Despawn(this);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer(_ballSettingsDatabase.BallLayerName) ||
                _rigidbody.isKinematic) return;
            
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.isKinematic = true;
                
            _chainManager.InsertBall(this);
        }

        public class Pool : MonoMemoryPool<Vector3, int, Ball>
        {
            protected override void Reinitialize(Vector3 pos, int colorId, Ball ball)
            {
                ball.OnSpawned(pos, colorId);
            }

            protected override void OnDespawned(Ball ball)
            {
                ball.OnDespawned();
            }
        }
    }
}