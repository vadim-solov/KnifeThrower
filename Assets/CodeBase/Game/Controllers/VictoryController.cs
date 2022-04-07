using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Collection;
using CodeBase.Factories;
using CodeBase.Game.Counters;
using CodeBase.ObjectType;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Game.Controllers
{
    public class VictoryController
    {
        private const float DestructionTime = 2f;
        
        private readonly GameFactory _gameFactory;
        private readonly KnivesCounter _knivesCounter;
        private readonly List<Knife> _knivesList;
        private readonly KnivesCollection _knivesCollection;
        private readonly StagesCounter _stagesCounter;
        private readonly Skins _skins;
        private readonly UIFactory _uiFactory;

        public VictoryController(GameFactory gameFactory, KnivesCounter knivesCounter, KnivesCollection knivesCollection, StagesCounter stagesCounter, Skins skins, UIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _knivesCounter = knivesCounter;
            _knivesList = knivesCollection.KnivesList;
            _knivesCollection = knivesCollection;
            _stagesCounter = stagesCounter;
            _skins = skins;
            _uiFactory = uiFactory;
            _knivesCounter.Victory += OnVictory;
        }
        
        public void Cleanup() => 
            _knivesCounter.Victory -= OnVictory;


        private void OnVictory()
        {
            DestroyBeam();
            _gameFactory.CreateParticles();
            TryDestroyApple();
            DestroyKnives();
            _knivesCollection.Clear();
            _skins.CheckNewSkins(_stagesCounter.Stage);
            
            if (_stagesCounter.CheckMaxStage() == true)
            {
                CreateMaxStageScreen();
                return;
            }

            CreateNewObjects();
        }

        private async void CreateMaxStageScreen()
        {
            await Task.Delay(TimeSpan.FromSeconds(DestructionTime));
            _uiFactory.CreateMaxStageScreen();
        }

        private async void CreateNewObjects()
        {
            await Task.Delay(TimeSpan.FromSeconds(DestructionTime));
            _stagesCounter.IncreaseStage();
            _knivesCounter.UpdateCounter();
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
        }

        private void DestroyBeam() => 
            _gameFactory.DestroyBeam();

        private void TryDestroyApple()
        {
            if(_gameFactory.Apple == null)
                return;
            
            Motion apple = _gameFactory.Apple.GetComponent<Motion>();
            apple.Drop();
            _gameFactory.DestroyApple(DestructionTime);
        }

        private void DestroyKnives()
        {
            foreach (Knife knife in _knivesList)
            {
                Motion motion = knife.GetComponent<Motion>();
                motion.Drop();
                _gameFactory.DestroyKnife(knife, DestructionTime);
            }
        }
    }
}