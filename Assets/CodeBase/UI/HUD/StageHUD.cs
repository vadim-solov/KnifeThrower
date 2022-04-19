using CodeBase.Configs;
using CodeBase.Factories;
using CodeBase.Game.Controllers;
using CodeBase.Game.Counters;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.HUD
{
    public class StageHUD : MonoBehaviour
    {
        private const string Boss = "BOSS ";
        private const string Stage = "STAGE ";

        [SerializeField]
        private Text _stageText;

        private GameFactory _gameFactory;
        private StagesCounter _stagesCounter;
        private VictoryController _victoryController;
        private StageConfig[] _stageConfig;

        public Text StageText => _stageText;

        public void Initialize(GameFactory gameFactory, StagesCounter stagesCounter, VictoryController victoryController)
        {
            _gameFactory = gameFactory;
            _stagesCounter = stagesCounter;
            _victoryController = victoryController;
            _stageConfig = _gameFactory.StageConfig;
            OnNewObjectsCreated();
            _gameFactory.EnemyCreated += OnNewObjectsCreated;
            _victoryController.Victory += OnVictory;
        }

        private void OnDisable()
        {
            _gameFactory.EnemyCreated -= OnNewObjectsCreated;
            _victoryController.Victory -= OnVictory;
        }

        private void OnNewObjectsCreated()
        {
            var isBoss = _stageConfig[_stagesCounter.CurrentStage].Boss;

            if (isBoss)
                _stageText.text = Boss + _stageConfig[_stagesCounter.CurrentStage].Name;
            
            else
                _stageText.text = Stage + (_stagesCounter.CurrentStage + 1);
            
            _stageText.GetComponent<Animator>().SetBool("StartAppearance", true);
            _stageText.GetComponent<Animator>().SetBool("StartFading", false);
        }

        private void OnVictory()
        {
            _stageText.GetComponent<Animator>().SetBool("StartFading", true);
            _stageText.GetComponent<Animator>().SetBool("StartAppearance", false);
        }
    }
}