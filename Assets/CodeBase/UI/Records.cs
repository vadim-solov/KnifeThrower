using CodeBase.SaveLoadSystem;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class Records : MonoBehaviour
    {
        [SerializeField]
        private Text _stageText;
        [SerializeField]
        private Text _scoreText;

        private ISaveLoadSystem _saveLoadSystem;

        public void Initialize(ISaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
            ShowScore();
            ShowStage();
        }

        private void ShowScore() => 
            _scoreText.text = _saveLoadSystem.LoadScore().ToString();
        
        private void ShowStage() => 
            _stageText.text = (_saveLoadSystem.LoadStage() + 1).ToString();
    }
}