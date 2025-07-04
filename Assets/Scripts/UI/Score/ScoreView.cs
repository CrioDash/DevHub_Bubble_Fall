using TMPro;
using UI.Impls;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Score
{
    public class ScoreView : UIView
    {
        [SerializeField] private TMP_Text scoreText;
        
        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}