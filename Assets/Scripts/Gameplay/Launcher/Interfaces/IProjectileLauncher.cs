using UnityEngine;

namespace Gameplay.Launcher.Interfaces
{
    public interface IProjectileLauncher
    {
        void Launch(int colorId, Vector3 origin, Vector3 direction);
    }
}