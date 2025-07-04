using Gameplay.Launcher.Interfaces;
using UnityEngine;

namespace Gameplay.Launcher.Impls
{
    public class LauncherPlaneAimer : IAimCalculator
    {
        public float GetAimAngle(Vector2 screenPos, Transform launcher)
        {
            Plane plane = new Plane(launcher.forward, launcher.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!plane.Raycast(ray, out float enter)) return 0;
            
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 local = launcher.InverseTransformPoint(hitPoint);
            
            float angle = Mathf.Atan2(local.y, local.x) * Mathf.Rad2Deg;

            angle = Mathf.Clamp(angle, 17.5f, 162.5f);
            
            return angle;
        }

        public Vector3 GetAimDirection(float angleDeg, Transform launcher)
        {
            float rad = angleDeg * Mathf.Deg2Rad;
            var localDir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f);
            return launcher.TransformDirection(localDir).normalized;
        }
    }
}