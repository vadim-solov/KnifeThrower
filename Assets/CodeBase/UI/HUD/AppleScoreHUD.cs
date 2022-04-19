using CodeBase.Game.Counters;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.HUD
{
    public class AppleScoreHUD : MonoBehaviour
    {
        [SerializeField]
        private Text _score;

        private AppleCounter _appleCounter;

        public void Initialize(AppleCounter appleCounter)
        {
            _appleCounter = appleCounter;
            ChangeScore(_appleCounter.Score);
            _appleCounter.ScoreChanged += ChangeScore;
        }

        private void OnDisable() => 
            _appleCounter.ScoreChanged -= ChangeScore;

        private void ChangeScore(int score) => 
            _score.text = score.ToString();
    }
}