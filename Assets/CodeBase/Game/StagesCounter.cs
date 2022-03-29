using UnityEngine;

namespace CodeBase.Game
{
    public class StagesCounter
    {
        private int _maxStage;
        
        public int CurrentStage { get; private set; } = 0;

        public void AddStage()
        {
            CurrentStage++;
            Debug.Log("Current stage " + CurrentStage);
        }

        public void ResetStages()
        {
            CurrentStage = 0;
            Debug.Log("Current stage " + CurrentStage);
        }
    }
}