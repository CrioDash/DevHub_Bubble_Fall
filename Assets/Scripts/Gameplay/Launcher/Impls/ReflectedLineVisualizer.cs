using System.Collections.Generic;
using Db;
using Gameplay.Launcher.Interfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.Launcher.Impls
{
    [RequireComponent(typeof(LineRenderer))]
    public class ReflectedLineVisualizer : MonoBehaviour, IAimVisualizer
    {
        [Header("Bounce Settings")] 
        [SerializeField] private int maxBounces = 1;
        [SerializeField] private float maxDistance = 50f;

        [Header("Tags")]
        public string wallTag = "Wall";
        public string ballTag = "Ball";
            
        private LineRenderer _lr;

        [Inject] private IBallSettingsDatabase _ballSettingsDatabase;
        [Inject] private IColorSwitcher _colorSwitcher;

        private void Awake()
        {
            _lr = GetComponent<LineRenderer>();
            _lr.useWorldSpace = true;
            _lr.positionCount = 0;
            
            _colorSwitcher.OnColorChanged += id =>
            {
                var color = BallColorPalette.GetColorById(id);
                _lr.material.color = color;
            };

            _lr.material.color = BallColorPalette.GetColorById(_colorSwitcher.CurrentColorId);
        }

        public void ShowTrajectory(Vector3 origin, Vector3 direction)
        {
            List<Vector3> points = new List<Vector3>();
            Vector3 pos = origin;
            Vector3 dir = direction.normalized;
            
            points.Add(origin);

            for (int i = 0; i < maxBounces; i++)
            {
                if (!Physics.Raycast(pos, dir, out var hit, maxDistance)) 
                {
                    points.Add(pos + dir * maxDistance);
                    break;
                }

                points.Add(hit.point);
                
                if (hit.collider.CompareTag(ballTag))
                    break;
                
                if (hit.collider.CompareTag(wallTag))
                {
                    dir = Vector3.Reflect(dir, hit.normal);
                    pos = hit.point + hit.normal * _ballSettingsDatabase.BallSpacing;
                    continue;
                }
                
                break;
            }
            
            _lr.positionCount = points.Count;
            _lr.SetPositions(points.ToArray());
        }

        public void HideTrajectory()
        {
            _lr.positionCount = 0;
        }
    }

}