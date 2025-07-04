using System;
using UI.Impls;
using Zenject;

namespace UI.Score
{
    public class ScorePresenter : UIPresenter<ScoreView, ScoreModel>, IInitializable, IDisposable
    {
        public void Initialize()
        {
            Model.OnScoreChanged += View.SetScore;
            View.SetScore(Model.Score);
            View.Show();
        }
        
        public void Dispose()
        {
            Model.OnScoreChanged -= View.SetScore;
        }
    }
}