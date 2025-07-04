using System.Collections.Generic;

namespace Core.Strategies
{
    public interface IBallRemovalStrategy
    {
        List<AxialCoord> FindCellsToRemove(AxialCoord start, GridManager grid);
    }
}