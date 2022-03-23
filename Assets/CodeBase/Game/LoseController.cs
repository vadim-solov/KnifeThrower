using System.Collections.Generic;
using CodeBase.Collection;
using CodeBase.Factories;
using CodeBase.ObjectType;
using UnityEngine;

namespace CodeBase.Game
{
    public class LoseController
    {
        private readonly GameFactory _gameFactory;
        private readonly UIFactory _uiFactory;
        private readonly HitController _hitController;
        private readonly KnivesCollection _knivesCollection;
        private readonly List<Knife> _knivesList;

        public LoseController(GameFactory gameFactory, UIFactory uiFactory, HitController hitController, KnivesCollection knivesCollection)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _hitController = hitController;
            _knivesCollection = knivesCollection;
            _knivesList = knivesCollection.KnivesList;
            _hitController.Lose += OnLose;
        }
        
        public void Cleanup()
        {
            Debug.Log("Desub");
            _hitController.Lose -= OnLose;
        }

        private void OnLose()
        {
            Debug.Log("Lose");
            StopMotion();
            _knivesCollection.Clear();
            _uiFactory.CreateLoseScreen();
        }

        private void StopMotion()
        {
            
        }
    }
}