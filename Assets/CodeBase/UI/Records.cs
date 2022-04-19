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
        private Text _gameProgressText;

        private ISaveLoadSystem _saveLoadSystem;

        public void Initialize(ISaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
            ShowGameProgress();
        }

        private void ShowGameProgress() => 
            _gameProgressText.text = Stage + (_saveLoadSystem.Load(SaveLoadType.MaxCompletedStage) + 1) + "  |  " + Score + _saveLoadSystem.Load(SaveLoadType.Score);
    }
}