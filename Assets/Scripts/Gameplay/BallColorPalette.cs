using UnityEngine;

namespace Gameplay
{
    public static class BallColorPalette
    {
        static readonly Color[] _colors = new Color[]
        {
            Color.red,
            Color.green,
            Color.yellow,
        };
        
        public static Color GetColorById(int id)
        {
            int idx = Mathf.Clamp(id, 0, _colors.Length - 1);
            return _colors[idx];
        }
        
        public static int Count => _colors.Length;
    }
}