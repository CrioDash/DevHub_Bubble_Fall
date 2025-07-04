using TMPro;
using UI.Impls;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Defeat
{
    public class DefeatView : UIView
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Button defeatButton;

        public Button DefeatButton => defeatButton;
        
        public void SetScore(int score)
        {
            scoreText.text = $"your score: {score.ToString()}";
        }
    }
}