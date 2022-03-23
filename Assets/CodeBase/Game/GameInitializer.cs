using System;
using CodeBase.Collection;
using CodeBase.Factories;
using UnityEngine;

namespace CodeBase.Game
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameFactory _gameFactory;
        [SerializeField]
        private UIFactory _uiFactory;
        [SerializeField]
        private HitController _hitController;

        private KnivesCounter _knivesCounter;
        private VictoryController _victoryController;
        private KnivesCollection _knivesCollection;
        private LoseController _loseController;
        
        private void Awake()
        {
            _knivesCollection = new KnivesCollection(_gameFactory);
            _gameFactory.Initialize(_hitController);
            _gameFactory.CreateContainer();
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateKnives();
            _gameFactory.CreatePlayerKnife();
            _uiFactory.CreateCanvas();

            _knivesCounter = new KnivesCounter(_gameFactory.StageConfig[0].NumberOfKnives);
            _hitController.Initialize(_gameFactory, _knivesCounter);
            _victoryController = new VictoryController(_gameFactory, _knivesCounter, _knivesCollection);
            _loseController = new LoseController(_gameFactory, _uiFactory, _hitController, _knivesCollection);
        }

        private void Start() => 
            Application.targetFrameRate = 300;

        private void OnDisable()
        {
            _knivesCollection.Cleanup();
            _victoryController.Cleanup();
            _loseController.Cleanup();
        }
    }
}