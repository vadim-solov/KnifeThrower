using CodeBase.Collection;
using CodeBase.Factories;
using CodeBase.Game.Controllers;
using CodeBase.Game.Counters;
using CodeBase.Game.Hit;
using CodeBase.SaveLoadSystem;
using CodeBase.Vibration;
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
        [SerializeField, Range(0f, 3f)]
        private float _delayBetweenShots = 0f;

        private KnivesCounter _knivesCounter;
        private VictoryController _victoryController;
        private KnivesCollection _knivesCollection;
        private LoseController _loseController;
        private StagesCounter _stagesCounter;
        private AppleHit _appleHit;
        private AppleCounter _appleCounter;
        private EnemyHit _enemyHit;
        private ScoreCounter _scoreCounter;

        private ISaveLoadSystem _saveLoadSystem;
        private IGameFactory _iGameFactory;
        private IUIFactory _iUiFactory;
        
        private void Awake()
        {
            _iUiFactory = _uiFactory;
            _iGameFactory = _gameFactory;
            _saveLoadSystem = new PlayerPrefsSystem();
            _stagesCounter = new StagesCounter(_saveLoadSystem, _iGameFactory);
            _skins.Initialize(_iUiFactory, _saveLoadSystem, _stagesCounter);
            _scoreCounter = new ScoreCounter(_saveLoadSystem);
            _appleCounter = new AppleCounter(_saveLoadSystem);
            _appleHit = new AppleHit(_appleCounter, _iGameFactory);
            _knivesCollection = new KnivesCollection(_iGameFactory);
            _knivesCounter = new KnivesCounter(_iGameFactory, _stagesCounter);
            _loseController = new LoseController(_iGameFactory, _iUiFactory, _knivesCollection, _stagesCounter, _knivesCounter, _scoreCounter);
            _victoryController = new VictoryController(_iGameFactory, _knivesCounter, _knivesCollection, _stagesCounter, _skins, _iUiFactory);
            _enemyHit = new EnemyHit(_knivesCounter, _iGameFactory, _scoreCounter, _victoryController, _delayBetweenShots);

            _iGameFactory.Initialize(_loseController, _stagesCounter, _appleHit, _enemyHit, _skins);
            _iUiFactory.Initialize(_appleCounter, _knivesCounter, _stagesCounter, _iGameFactory, _scoreCounter, _saveLoadSystem, _skins, _cameraPrefab, _victoryController);
            _iUiFactory.CreateCamera();

            _iUiFactory.CreateCanvas();
            _iUiFactory.CreateStartScreen();

            MainVibration.Init();
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