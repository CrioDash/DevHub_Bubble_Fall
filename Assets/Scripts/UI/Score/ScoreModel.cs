using System;
using UI.Base;

namespace UI.Score
{
    public class ScoreModel : IUIModel
    {
        public event Action<int> OnScoreChanged = delegate { };
        private int _score;
        public int Score => _score;

        public void AddScore(int delta)
        {
            if (delta <= 0) return;
            _score += delta;
            OnScoreChanged(_score);
        }
    }
}