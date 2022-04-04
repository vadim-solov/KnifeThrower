using CodeBase.SaveLoadSystem;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class Records : MonoBehaviour
    {
        private const string Stage = "STAGE ";
        private const string Score = "SCORE ";

        [SerializeField]
        private Text _stageText;
        [SerializeField]
        private Text _scoreText;

        private ISaveLoadSystem _saveLoadSystem;

        public void Initialize(ISaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
            ShowStage();
            ShowScore();
        }

        private void ShowStage() => 
            _stageText.text = Stage + (_saveLoadSystem.LoadStage() + 1).ToString();

        private void ShowScore() => 
            _scoreText.text = Score + _saveLoadSystem.LoadScore().ToString();
    }
}