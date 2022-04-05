using System;
using CodeBase.Configs;
using CodeBase.Factories;
using CodeBase.SaveLoadSystem;

namespace CodeBase.Game.Counters
{
    public class StagesCounter
    {
        private readonly ISaveLoadSystem _saveLoadSystem;
        private readonly GameFactory _gameFactory;

        public int Stage { get; private set; } = 0;

        public event Action<int> StageChanged;

        public StagesCounter(ISaveLoadSystem saveLoadSystem, GameFactory gameFactory)
        {
            _saveLoadSystem = saveLoadSystem;
            _gameFactory = gameFactory;
        }

        public void IncreaseStage()
        {
            Stage++;
            StageChanged?.Invoke(Stage);
        }

        public void ResetStages()
        {
            SaveMaxStage();
            Stage = 0;
            StageChanged?.Invoke(Stage);
        }
        
        private void SaveMaxStage()
        {
            int loadedStage = _saveLoadSystem.LoadStage();
            
            if (loadedStage < Stage)
                _saveLoadSystem.SaveStage(Stage);
        }

        public bool CheckMaxStage()
        {
            if (Stage >= _gameFactory.StageConfig.Length - 1)
                return true;

            return false;
        }
    }
}