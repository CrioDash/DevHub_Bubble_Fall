using System;
using UI.Impls;
using UI.Score;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UI.Defeat
{
    public class DefeatPresenter : UIPresenter<DefeatView, DefeatModel>, IInitializable, IDisposable
    {
        private readonly ScoreModel _scoreModel;

        public DefeatPresenter(ScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
        }
        
        public void Initialize()
        {
            Model.OnShow += OnShow;
            
            View.DefeatButton.onClick.AddListener( OnDefeatClick);
            
            View.Hide();
        }
        
        public void Dispose()
        {
            Model.OnShow -= OnShow;
        }

        private void OnShow()
        {
            Show();
            View.SetScore(_scoreModel.Score);
            Time.timeScale = 0;
        }

        private void OnDefeatClick()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        
    }
}