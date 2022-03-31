using System;
using CodeBase.Factories;
using CodeBase.Game;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.HUD
{
    public class StageHUD : MonoBehaviour
    {
        [SerializeField]
        private Text _stageText;

        private StageConfig[] _stageConfig;
        private StagesCounter _stagesCounter;
        private GameFactory _gameFactory;

        public void Initialize(StageConfig[] stageConfig, StagesCounter stagesCounter, GameFactory gameFactory)
        {
            _stageConfig = stageConfig;
            _stagesCounter = stagesCounter;
            _gameFactory = gameFactory;

            _gameFactory.BeamCreated += OnBossCreated;
        }

        private void OnBossCreated()
        {
            var boss = _stageConfig[_stagesCounter.CurrentStage].Boss;

            if (boss)
            {
                _stageText.text = _stageConfig[_stagesCounter.CurrentStage].Name;
            }
        }
    }
}