#if UNITY_EDITOR
using UnityEditor;
#endif
using Core;
using UnityEngine;
using Zenject;

[ExecuteAlways]
public class GridDebugger : MonoBehaviour
{
    [Inject] private GridManager _grid;
    
    public bool showNeighborsAlways = false;
    public bool showNeighborsOnSelect = true;
    
    public float sphereRadius = 0.1f;

    private void OnDrawGizmos()
    {
        if (_grid == null) return;

        var cells = _grid.Cells;
        
        foreach (var (coord, ball) in cells)
        {
            Vector3 pos = _grid.HexToWorld(coord);

            Gizmos.color = ball.BallColor;
            Gizmos.DrawSphere(pos, sphereRadius);
            
            #if UNITY_EDITOR
                Handles.color = Color.white;
                Handles.Label(pos + Vector3.up * (sphereRadius*1.5f),
                    ball.ColorId.ToString());
            #endif
        }

        if (!showNeighborsAlways && !showNeighborsOnSelect) return;
        {
            foreach (var (coord, ball) in cells)
            {
                Vector3 pos = _grid.HexToWorld(coord);
                
                #if UNITY_EDITOR
                    if (showNeighborsOnSelect && !showNeighborsAlways)
                    {
                        if (Selection.activeGameObject != ball.gameObject)
                            continue;
                    }
                #endif

                Gizmos.color = Color.black;
                foreach (var nb in coord.GetNeighbors())
                {
                    if (!cells.TryGetValue(nb, out var neighborBall)) continue;
                    Vector3 npos = _grid.HexToWorld(nb);
                    Gizmos.DrawLine(pos, npos);
                }
            }
        }
    }
}