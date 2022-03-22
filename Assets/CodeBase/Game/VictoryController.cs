using System.Collections.Generic;
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

        public VictoryController(GameFactory gameFactory, KnivesCounter knivesCounter, KnivesCollection knivesCollection)
        {
            _gameFactory = gameFactory;
            _knivesCounter = knivesCounter;
            _knivesList = knivesCollection.KnivesList;
            _knivesCollection = knivesCollection;
            _knivesCounter.Victory += OnVictory;
        }

        private void OnVictory()
        {
            Debug.Log("Victory!");
            _gameFactory.DestroyBeam();
            _gameFactory.DestroyApple();
            SplashKnives();
            DestroyKnives();
            _knivesCollection.Clear();
        }

        private void SplashKnives()
        {
            foreach (Knife knife in _knivesList)
                knife.GetComponent<Motion>().Splash();
        }

        private void DestroyKnives()
        {
            foreach (Knife knife in _knivesList)
            {
                _gameFactory.DestroyKnife(knife);
                Debug.Log("Knife destroyed");                
            }
        }
    }
}