using System;
using Db;
using UnityEngine;
using Zenject;

namespace Core
{
    public class BallMover : MonoBehaviour
    {
        [Inject] private IBallSettingsDatabase _ballSettingsDatabase;
        
        private void Update()
        {
            Vector3 downDir = -transform.up;
            
            transform.position += downDir * (_ballSettingsDatabase.BallSpeed * Time.deltaTime);
        }
    }
}