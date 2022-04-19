using CodeBase.Game.Counters;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.HUD
{
    public class ScoreHUD : MonoBehaviour
    {
        [SerializeField]
        private Text _scoreText;

        private ScoreCounter _scoreCounter;

        public Text ScoreText => _scoreText;

        public void Initialize(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
            ChangeScore(_scoreCounter.Score);
            _scoreCounter.ScoreChanged += ChangeScore;
        }

        private void OnDisable() => 
            _scoreCounter.ScoreChanged -= ChangeScore;

        private void ChangeScore(int score) => 
            _scoreText.text = score.ToString();
    }
}