using System;
using CodeBase.Game;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.HUD
{
    public class AppleScoreHUD : MonoBehaviour
    {
        [SerializeField]
        private Text _score;

        private AppleCounter _appleCounter;

        public void Initialize(AppleCounter appleCounter) => 
            _appleCounter = appleCounter;

        private void Start()
        {
            _score.text = _appleCounter.Score.ToString();
            _appleCounter.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable() => 
            _appleCounter.ScoreChanged -= OnScoreChanged;

        private void OnScoreChanged(int score) => 
            _score.text = score.ToString();
    }
}