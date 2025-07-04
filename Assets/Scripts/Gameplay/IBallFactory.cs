using UnityEngine;

namespace Gameplay
{
    public interface IBallFactory
    {
        Ball Create(Vector3 position, int colorId);
    }
}