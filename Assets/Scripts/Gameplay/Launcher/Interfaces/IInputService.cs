using System;
using UnityEngine;

namespace Gameplay.Launcher.Interfaces
{
    public interface IInputService
    {
        event Action<Vector2> OnClick;
        event Action<Vector2> OnDrag;
        event Action<Vector2> OnRelease;
    }
}