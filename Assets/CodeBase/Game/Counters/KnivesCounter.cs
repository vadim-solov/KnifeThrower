using System;
using CodeBase.Factories;

namespace CodeBase.Game.Counters
{
    public class KnivesCounter
    {
        private readonly StagesCounter _stagesCounter;
        
        private readonly IGameFactory _gameFactory;

        public int NumberOfKnives { get; private set; }

        public event Action Victory;
        public event Action<int> DecreaseNumberOfKnives;
        
        public KnivesCounter(IGameFactory gameFactory, StagesCounter stagesCounter)
        {
            _gameFactory = gameFactory;
            _stagesCounter = stagesCounter;
            UpdateCounter();
        }

        public void Decrease()
        {
            NumberOfKnives--;
            DecreaseNumberOfKnives?.Invoke(NumberOfKnives);
            
            if(NumberOfKnives <= 0)
                Victory?.Invoke();            
        }

        public bool CheckLastKnife()
        {
            if (NumberOfKnives <= 0)
                return true;
            
            return false;
        }

        public void UpdateCounter() => 
            NumberOfKnives = _gameFactory.StageConfig[_stagesCounter.CurrentStage].NumberOfKnives;
    }
}