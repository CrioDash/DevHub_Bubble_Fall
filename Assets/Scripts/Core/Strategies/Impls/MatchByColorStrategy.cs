using System.Collections.Generic;

namespace Core.Strategies.Impls
{
    public class MatchByColorStrategy : IBallRemovalStrategy
    {
        public List<AxialCoord> FindCellsToRemove(AxialCoord start, GridManager grid)
        {
            if (!grid.Cells.TryGetValue(start, out var startBall))
                return new List<AxialCoord>();

            var targetColor = startBall.BallColor;
            var visited = new HashSet<AxialCoord> { start };
            var queue = new Queue<AxialCoord>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var cell = queue.Dequeue();
                foreach (var nb in cell.GetNeighbors())
                {
                    if (visited.Contains(nb)
                        || !grid.Cells.TryGetValue(nb, out var b)
                        || b.BallColor != targetColor) continue;
                    visited.Add(nb);
                    queue.Enqueue(nb);
                }
            }
            
            return visited.Count >= 3
                ? new List<AxialCoord>(visited)
                : new List<AxialCoord>();
        }
    }
}