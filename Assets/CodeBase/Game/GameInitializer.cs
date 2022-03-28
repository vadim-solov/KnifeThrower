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

        private KnivesCounter _knivesCounter;
        private VictoryController _victoryController;
        private KnivesCollection _knivesCollection;
        private LoseController _loseController;
        
        private void Awake()
        {
            _knivesCollection = new KnivesCollection(_gameFactory);
            _knivesCounter = new KnivesCounter(_gameFactory.StageConfig[0].NumberOfKnives);
            _loseController = new LoseController(_gameFactory, _uiFactory, _knivesCollection);
            _victoryController = new VictoryController(_gameFactory, _knivesCounter, _knivesCollection);

            _gameFactory.Initialize(_loseController, _victoryController);
            _gameFactory.CreateContainer();
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
            _uiFactory.Initialize(_gameFactory);
            _uiFactory.CreateCanvas();
        }

        private void Start() => 
            Application.targetFrameRate = 300;

        private void OnDisable()
        {
            _knivesCollection.Cleanup();
            _victoryController.Cleanup();
        }
    }
}