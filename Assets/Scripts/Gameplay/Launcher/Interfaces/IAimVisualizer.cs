using UnityEngine;

namespace Gameplay.Launcher.Interfaces
{
    public interface IAimVisualizer
    {
        void ShowTrajectory(Vector3 origin, Vector3 direction);
        void HideTrajectory();
    }
}