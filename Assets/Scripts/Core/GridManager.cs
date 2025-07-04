using System.Collections.Generic;
using Db;
using Gameplay;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GridManager
    {
        public readonly Dictionary<AxialCoord, Ball> Cells = new();

        private readonly Transform _origin;
        private readonly IBallSettingsDatabase _ballSettingsDatabase;

        public GridManager([Inject(Id = "Balls")] Transform origin, IBallSettingsDatabase ballSettingsDatabase)
        {
            _origin = origin;
            _ballSettingsDatabase = ballSettingsDatabase;
        }
        
        public AxialCoord WorldToHex(Vector3 worldPos)
        {
            Vector3 local = _origin.InverseTransformPoint(worldPos);
            
            int r = Mathf.RoundToInt(local.y / (_ballSettingsDatabase.BallSpacing * Mathf.Sqrt(3f) / 2f));
            
            bool isOdd = (r & 1) != 0;
            float xOffset = isOdd ? _ballSettingsDatabase.BallSpacing * 0.5f : 0f;
            
            int q = Mathf.RoundToInt((local.x - xOffset) / _ballSettingsDatabase.BallSpacing);

            return new AxialCoord(q, r);
        }
        
        public Vector3 HexToWorld(AxialCoord coord)
        {
            float yLocal = coord.R * (_ballSettingsDatabase.BallSpacing * Mathf.Sqrt(3f) / 2f);
            bool  isOdd = (coord.R & 1) != 0;
            if (!isOdd)
                coord.Q = Mathf.Clamp(coord.Q, -_ballSettingsDatabase.ColumnsCount / 2 + 1,
                    _ballSettingsDatabase.ColumnsCount/2);
            else
                coord.Q = Mathf.Clamp(coord.Q, -_ballSettingsDatabase.ColumnsCount / 2,
                    _ballSettingsDatabase.ColumnsCount/2 - 1);
            float xLocal = coord.Q * _ballSettingsDatabase.BallSpacing + (isOdd ? _ballSettingsDatabase.BallSpacing * 0.5f : 0f);
            Vector3 local = new Vector3(xLocal, yLocal, 0f);
            return _origin.TransformPoint(local);
        }

        public void TryAdd(AxialCoord coord, Ball ball)
        {
            Cells.TryAdd(coord, ball);
        }

        public void Remove(AxialCoord coord)
        {
            Cells.Remove(coord);
        }
        
    }
}
