using System;
using Gameplay.Launcher.Interfaces;

namespace Gameplay.Launcher.Impls
{
    public class ColorSwitcher : IColorSwitcher
    {
        public event Action<int> OnColorChanged = delegate { };

        private int _colorA, _colorB;
        private bool _useA;

        public int CurrentColorId => _useA ? _colorA : _colorB;

        public void PickNewColors()
        {
            int count = BallColorPalette.Count;
            _colorA = UnityEngine.Random.Range(0, count);
            do {
                _colorB = UnityEngine.Random.Range(0, count);
            } while (_colorB == _colorA);
            _useA = true;
            OnColorChanged(CurrentColorId);
        }

        public void ToggleColor()
        {
            _useA = !_useA;
            OnColorChanged(CurrentColorId);
        }
    }
}