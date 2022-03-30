using UnityEngine;

namespace CodeBase.Game
{
    public class StagesCounter
    {
        private int _maxStage;
        
        public int CurrentStage { get; private set; } = 0;

        public void IncreaseStage() => 
            CurrentStage++;

        public void ResetStages() => 
            CurrentStage = 0;
    }
}