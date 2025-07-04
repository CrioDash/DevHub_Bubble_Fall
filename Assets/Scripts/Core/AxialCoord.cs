using System.Collections.Generic;

namespace Core
{
    public struct AxialCoord
    {
        public int Q, R;
        public AxialCoord(int q, int r) { this.Q = q; this.R = r; }
        
        private static readonly (int dq, int dr)[] EvenRow = {
            (+1,  0), ( 0, -1), (-1, -1),
            (-1,  0), (-1, +1), ( 0, +1),
        };

        private static readonly (int dq, int dr)[] OddRow = {
            (+1,  0), (+1, -1), ( 0, -1),
            (-1,  0), ( 0, +1), (+1, +1),
        };
        
        public IEnumerable<AxialCoord> GetNeighbors()
        {
            var deltas = (R & 1) == 1 ? OddRow : EvenRow;
            foreach (var d in deltas)
                yield return new AxialCoord(Q + d.dq, R + d.dr);
        }

        public override bool Equals(object obj)
            => obj is AxialCoord o && o.Q == Q && o.R == R;
        public override int GetHashCode()
            => (Q * 397) ^ R;
    }
}