using System;

namespace Gameplay.Launcher.Interfaces
{
    public interface IColorSwitcher
    {
        event Action<int> OnColorChanged;
        int CurrentColorId { get; }
        void PickNewColors();
        void ToggleColor();
    }
}