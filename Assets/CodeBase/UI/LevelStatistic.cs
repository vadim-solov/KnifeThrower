using CodeBase.Game;
using CodeBase.Game.Counters;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class LevelStatistic : MonoBehaviour
    {
        private const string Stage = "STAGE ";

        [SerializeField]
        private Text _scoreText;
        [SerializeField]
        private Text _stageText;

        private ScoreCounter _scoreCounter;
        private StagesCounter _stagesCounter;

        public void Initialize(ScoreCounter scoreCounter, StagesCounter stagesCounter)
        {
            _scoreCounter = scoreCounter;
            _stagesCounter = stagesCounter;
            ShowScore();
            ShowStage();
        }

        private void ShowScore() => 
            _scoreText.text = _scoreCounter.Score.ToString();

        private void ShowStage() => 
            _stageText.text = Stage + (_stagesCounter.CurrentStage + 1);
    }
}