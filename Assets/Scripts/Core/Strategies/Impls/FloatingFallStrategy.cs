using System.Collections.Generic;
using System.Linq;

namespace Core.Strategies.Impls
{
    public class FloatingFallStrategy : IBallRemovalStrategy
    {
        public List<AxialCoord> FindCellsToRemove(AxialCoord start, GridManager grid)
        {
            var all = grid.Cells.Keys.ToList();
            if (all.Count == 0)
                return new List<AxialCoord>();
            
            int anchorRow = all.Max(c => c.R);
            var anchors = all.Where(c => c.R == anchorRow).ToList();
            
            var visited = new HashSet<AxialCoord>(anchors);
            var queue = new Queue<AxialCoord>(anchors);
            while (queue.Count > 0)
            {
                var cell = queue.Dequeue();
                foreach (var nb in cell.GetNeighbors())
                {
                    if (visited.Contains(nb) || !grid.Cells.ContainsKey(nb))
                        continue;
                    visited.Add(nb);
                    queue.Enqueue(nb);
                }
            }
            
            return grid.Cells.Keys
                .Where(c => !visited.Contains(c))
                .ToList();
        }
    }
}