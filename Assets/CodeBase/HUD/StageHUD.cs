using CodeBase.Configs;
using CodeBase.Game.Counters;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.HUD
{
    public class StageHUD : MonoBehaviour
    {
        private const string Boss = "BOSS ";
        private const string Stage = "STAGE ";

        [SerializeField]
        private Text _stageText;

        private StagesCounter _stagesCounter;
        private StageConfig[] _stageConfig;

        public void Initialize(StagesCounter stagesCounter, StageConfig[] stageConfig)
        {
            _stagesCounter = stagesCounter;
            _stageConfig = stageConfig;
            OnBeamCreated(_stagesCounter.CurrentStage);
            _stagesCounter.StageChanged += OnBeamCreated;
        }

        private void OnDisable() => 
            _stagesCounter.StageChanged -= OnBeamCreated;

        private void OnBeamCreated(int stage)
        {
            var boss = _stageConfig[_stagesCounter.CurrentStage].Boss;

            if (boss)
                _stageText.text = Boss + _stageConfig[_stagesCounter.CurrentStage].Name;
            
            else
                _stageText.text = Stage + (stage + 1);
        }
    }
}