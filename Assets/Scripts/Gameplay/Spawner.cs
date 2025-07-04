using System.Collections;
using Core;
using Db;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnOrigin;

        private IBallFactory _factory;
        private GridManager _grid;
        private IBallSettingsDatabase _ballSettingsDatabase;

        private float _lastLocalY;
        private int _rowCount;

        [Inject]
        private void Construct(IBallFactory factory, GridManager grid, IBallSettingsDatabase ballSettingsDatabase)
        {
            _factory = factory;
            _grid = grid;
            _ballSettingsDatabase = ballSettingsDatabase;
        }

        private void Start()
        {
            _lastLocalY = 0f;
            _rowCount = 0;

            for(int i = 0; i < _ballSettingsDatabase.StartSpawnСount; i++)
            {
                SpawnNextRow();
            }
            
            StartCoroutine(SpawnWaves());
        }

        private IEnumerator SpawnWaves()
        {
            while (true)
            {
                SpawnNextRow();
                yield return new WaitForSeconds(_ballSettingsDatabase.SpawnInterval);
            }
        }

        private void SpawnNextRow()
        {
            float vStep = _ballSettingsDatabase.BallSpacing * Mathf.Sqrt(3f) / 2f;
            float newLocalY = _lastLocalY + vStep;
            bool isOddRow = (_rowCount & 1) != 0;
            float xOffsetLocal= isOddRow ? _ballSettingsDatabase.BallSpacing * 0.5f : 0f;
            float totalWidth = (_ballSettingsDatabase.ColumnsCount - 1) * _ballSettingsDatabase.BallSpacing;
            float startXLocal = -totalWidth / 2f + xOffsetLocal;

            for (int col = 0; col < _ballSettingsDatabase.ColumnsCount; col++)
            {
                float xLocal = startXLocal + col * _ballSettingsDatabase.BallSpacing;
                Vector3 localPos = new Vector3(xLocal, newLocalY, 0f);
                Vector3 worldPos = spawnOrigin.TransformPoint(localPos);
                
                AxialCoord coord = _grid.WorldToHex(worldPos);
                Vector3 center = _grid.HexToWorld(coord);
                
                int colorId = Random.Range(0, BallColorPalette.Count);
                var ball = _factory.Create(center, colorId);
                
                _grid.TryAdd(coord, ball);
            }
        

            _lastLocalY = newLocalY;
            _rowCount++;
        }
    }
}
