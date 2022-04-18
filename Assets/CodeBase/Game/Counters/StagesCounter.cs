using System;
using CodeBase.Factories;
using CodeBase.SaveLoadSystem;
using UnityEngine;

namespace CodeBase.Game.Counters
{
    public class StagesCounter
    {
        private readonly ISaveLoadSystem _saveLoadSystem;
        private readonly GameFactory _gameFactory;

        public int CurrentStage { get; private set; } = 0;
        public int MaxCompletedStage { get; private set; }

        public event Action<int> StageChanged;

        public StagesCounter(ISaveLoadSystem saveLoadSystem, GameFactory gameFactory)
        {
            _saveLoadSystem = saveLoadSystem;
            _gameFactory = gameFactory;
            MaxCompletedStage = _saveLoadSystem.LoadMaxCompletedStage();
            Debug.Log(MaxCompletedStage);
        }

        public void IncreaseStage()
        {
            CurrentStage++;
            if (CurrentStage > MaxCompletedStage)
            {
                MaxCompletedStage = CurrentStage;
                _saveLoadSystem.SaveMaxCompletedStage(MaxCompletedStage);
            }
                
            StageChanged?.Invoke(CurrentStage);
        }

        public void ResetStages()
        {
            SaveMaxStage();
            CurrentStage = 0;
            StageChanged?.Invoke(CurrentStage);
        }
        
        private void SaveMaxStage()
        {
            int maxStage = _saveLoadSystem.LoadMaxCompletedStage();

            if (maxStage < CurrentStage)
            {
                MaxCompletedStage = CurrentStage;
                _saveLoadSystem.SaveMaxCompletedStage(MaxCompletedStage);
            }
        }

        public bool CheckMaxCompletedStage()
        {
            if (CurrentStage >= _gameFactory.StageConfig.Length - 1)
            {
                MaxCompletedStage = CurrentStage + 1;
                _saveLoadSystem.SaveMaxCompletedStage(MaxCompletedStage);
                
                return true;
            }

            return false;
        }
    }
}