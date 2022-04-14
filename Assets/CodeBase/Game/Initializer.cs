using CodeBase.Collection;
using CodeBase.Factories;
using CodeBase.Game.Controllers;
using CodeBase.Game.Counters;
using CodeBase.SaveLoadSystem;
using UnityEngine;

namespace CodeBase.Game
{
    public class Initializer : MonoBehaviour
    {
        private const int TargetFrameRate = 300;
        
        [SerializeField]
        private GameFactory _gameFactory;
        [SerializeField]
        private UIFactory _uiFactory;
        [SerializeField]
        private Skins _skins;
        [SerializeField]
        private Camera _cameraPrefab;

        private KnivesCounter _knivesCounter;
        private VictoryController _victoryController;
        private KnivesCollection _knivesCollection;
        private LoseController _loseController;
        private StagesCounter _stagesCounter;
        private AppleHit _appleHit;
        private AppleCounter _appleCounter;
        private LogHit _logHit;
        private ScoreCounter _scoreCounter;

        private ISaveLoadSystem _saveLoadSystem;
        
        private void Awake()
        {
            _skins.Initialize(_uiFactory);
            _saveLoadSystem = new PlayerPrefsSystem();
            _stagesCounter = new StagesCounter(_saveLoadSystem, _gameFactory);
            _scoreCounter = new ScoreCounter(_saveLoadSystem);
            _appleCounter = new AppleCounter(_saveLoadSystem);
            _appleHit = new AppleHit(_appleCounter, _gameFactory);
            _knivesCollection = new KnivesCollection(_gameFactory);
            _knivesCounter = new KnivesCounter(_gameFactory, _stagesCounter);
            _loseController = new LoseController(_gameFactory, _uiFactory, _knivesCollection, _stagesCounter, _knivesCounter, _scoreCounter);
            _victoryController = new VictoryController(_gameFactory, _knivesCounter, _knivesCollection, _stagesCounter, _skins, _uiFactory);
            _logHit = new LogHit(_knivesCounter, _gameFactory, _scoreCounter);

            _gameFactory.Initialize(_loseController, _stagesCounter, _appleHit, _logHit);
            _uiFactory.Initialize(_appleCounter, _knivesCounter, _stagesCounter, _gameFactory, _scoreCounter, _saveLoadSystem, _skins, _cameraPrefab);
            _uiFactory.CreateCamera();

            _uiFactory.CreateCanvas();
            _uiFactory.CreateStartScreen();
        }

        private void Start() => 
            Application.targetFrameRate = TargetFrameRate;

        private void OnDisable()
        {
            _knivesCollection.Cleanup();
            _victoryController.Cleanup();
        }
    }
}