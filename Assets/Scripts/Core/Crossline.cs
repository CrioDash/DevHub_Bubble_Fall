using System;
using Db;
using UI.Defeat;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core
{
    public class Crossline : MonoBehaviour
    {
        [Inject] private IBallSettingsDatabase _ballSettingsDatabase;
        [Inject] private DefeatModel _defeatModel;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        void Update()
        {
            Physics.SyncTransforms();
            
            var b = _collider.bounds;
            
            int ballLayer = LayerMask.NameToLayer(_ballSettingsDatabase.BallLayerName);
            int layerMask = 1 << ballLayer;
            
            var hits = Physics.OverlapBox(
                b.center,
                b.extents,
                Quaternion.identity,
                layerMask);
            
            if (hits.Length > 0)
            {
                _defeatModel.Show();
                enabled = false;
            }
        }
    }
}