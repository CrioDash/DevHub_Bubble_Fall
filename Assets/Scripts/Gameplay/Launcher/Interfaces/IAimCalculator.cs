using UnityEngine;

namespace Gameplay.Launcher.Interfaces
{
    public interface IAimCalculator
    {
        float GetAimAngle(Vector2 screenPos, Transform launcher);
        Vector3 GetAimDirection(float angleDeg, Transform launcher);
    }
}