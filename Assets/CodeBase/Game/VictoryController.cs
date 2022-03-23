using System.Collections.Generic;
using CodeBase.Collection;
using CodeBase.Factories;
using CodeBase.ObjectType;
using UnityEngine;

namespace CodeBase.Game
{
    public class VictoryController
    {
        private readonly GameFactory _gameFactory;
        private readonly KnivesCounter _knivesCounter;
        private readonly List<Knife> _knivesList;
        private readonly KnivesCollection _knivesCollection;

        public VictoryController(GameFactory gameFactory, KnivesCounter knivesCounter, KnivesCollection knivesCollection)
        {
            _gameFactory = gameFactory;
            _knivesCounter = knivesCounter;
            _knivesList = knivesCollection.KnivesList;
            _knivesCollection = knivesCollection;
            _knivesCounter.Victory += OnVictory;
        }
        
        public void Cleanup()
        {
            _knivesCounter.Victory -= OnVictory;
            Debug.Log("Desub");
        }


        private void OnVictory()
        {
            Debug.Log("Victory!");
            DestroyBeam();
            DestroyApple();
            DestroyKnives();
            _knivesCollection.Clear();
            
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateKnives();
            _gameFactory.CreatePlayerKnife();
        }

        private void DestroyBeam() => 
            _gameFactory.DestroyBeam();

        private void DestroyApple() => 
            _gameFactory.DestroyApple();

        private void DestroyKnives()
        {
            foreach (Knife knife in _knivesList) 
                _gameFactory.DestroyKnife(knife);
        }
    }
}