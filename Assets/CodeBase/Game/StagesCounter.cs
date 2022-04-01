using System;
using CodeBase.SaveLoadSystem;

namespace CodeBase.Game
{
    public class StagesCounter
    {
        private readonly ISaveLoadSystem _saveLoadSystem;
        
        public int Stage { get; private set; } = 0;
        
        public StagesCounter(ISaveLoadSystem saveLoadSystem) => 
            _saveLoadSystem = saveLoadSystem;

        public event Action<int> StageChanged;

        public void IncreaseStage()
        {
            Stage++;
            StageChanged?.Invoke(Stage);
        }

        public void ResetStages()
        {
            CheckMaxStage();

            Stage = 0;
            StageChanged?.Invoke(Stage);
        }
        
        private void CheckMaxStage()
        {
            var loadedStage = _saveLoadSystem.LoadStage();
            
            if (loadedStage < Stage)
                _saveLoadSystem.SaveStage(Stage);
        }
    }
}