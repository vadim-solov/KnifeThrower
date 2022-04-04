using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Behaviours;
using CodeBase.Collection;
using CodeBase.Factories;
using CodeBase.ObjectType;
using UnityEngine;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Game
{
    public class VictoryController
    {
        private readonly GameFactory _gameFactory;
        private readonly KnivesCounter _knivesCounter;
        private readonly List<Knife> _knivesList;
        private readonly KnivesCollection _knivesCollection;
        private readonly StagesCounter _stagesCounter;
        private readonly Skins _skins;

        public VictoryController(GameFactory gameFactory, KnivesCounter knivesCounter, KnivesCollection knivesCollection, StagesCounter stagesCounter, Skins skins)
        {
            _gameFactory = gameFactory;
            _knivesCounter = knivesCounter;
            _knivesList = knivesCollection.KnivesList;
            _knivesCollection = knivesCollection;
            _stagesCounter = stagesCounter;
            _skins = skins;
            _knivesCounter.Victory += OnVictory;
        }
        
        public void Cleanup() => 
            _knivesCounter.Victory -= OnVictory;


        private void OnVictory()
        {
            DestroyBeam();
            TryDestroyApple();
            DestroyKnives();
            _knivesCollection.Clear();
            _skins.CheckNewSkins(_stagesCounter.Stage);
            CreateNewObjects();
        }

        private async void CreateNewObjects()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
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
            
            var apple = _gameFactory.Apple.GetComponent<Motion>();
            apple.Drop();
            _gameFactory.DestroyApple(2f);
        }

        private void DestroyKnives()
        {
            foreach (Knife knife in _knivesList)
            {
                var motion = knife.GetComponent<Motion>();
                motion.Drop();
                _gameFactory.DestroyKnife(knife, 2f);
            }
        }
    }
}