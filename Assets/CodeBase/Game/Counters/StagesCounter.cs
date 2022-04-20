using CodeBase.Factories;
using CodeBase.SaveLoadSystem;

namespace CodeBase.Game.Counters
{
    public class StagesCounter
    {
        private readonly ISaveLoadSystem _saveLoadSystem;
        private readonly IGameFactory _gameFactory;

        public int CurrentStage { get; private set; } = 0;
        public int MaxCompletedStage { get; private set; }

        public StagesCounter(ISaveLoadSystem saveLoadSystem, IGameFactory gameFactory)
        {
            _saveLoadSystem = saveLoadSystem;
            _gameFactory = gameFactory;
            MaxCompletedStage = _saveLoadSystem.Load(SaveLoadType.MaxCompletedStage);
        }

        public void IncreaseStage()
        {
            CurrentStage++;
            if (CurrentStage > MaxCompletedStage)
            {
                MaxCompletedStage = CurrentStage;
                _saveLoadSystem.Save(SaveLoadType.MaxCompletedStage, MaxCompletedStage);
            }
        }

        public void ResetStages()
        {
            SaveMaxStage();
            CurrentStage = 0;
        }
        
        private void SaveMaxStage()
        {
            int maxStage = _saveLoadSystem.Load(SaveLoadType.MaxCompletedStage);

            if (maxStage < CurrentStage)
            {
                MaxCompletedStage = CurrentStage;
                _saveLoadSystem.Save(SaveLoadType.MaxCompletedStage, MaxCompletedStage);
            }
        }

        public bool CheckMaxCompletedStage()
        {
            if (CurrentStage >= _gameFactory.StageConfig.Length - 1)
            {
                MaxCompletedStage = CurrentStage + 1;
                _saveLoadSystem.Save(SaveLoadType.MaxCompletedStage, MaxCompletedStage);
                
                return true;
            }

            return false;
        }
    }
}