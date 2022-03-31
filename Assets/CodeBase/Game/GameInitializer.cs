using CodeBase.Collection;
using CodeBase.Factories;
using CodeBase.SaveLoadSystem;
using UnityEngine;

namespace CodeBase.Game
{
    public class GameInitializer : MonoBehaviour
    {
        private const int TargetFrameRate = 300;
        
        [SerializeField]
        private GameFactory _gameFactory;
        [SerializeField]
        private UIFactory _uiFactory;

        private KnivesCounter _knivesCounter;
        private VictoryController _victoryController;
        private KnivesCollection _knivesCollection;
        private LoseController _loseController;
        private StagesCounter _stagesCounter;
        private AppleHit _appleHit;
        private AppleCounter _appleCounter;
        private RestarterController _restarterController;

        private ISaveLoadSystem _saveLoadSystem;
        
        private void Awake()
        {
            _restarterController = new RestarterController(_gameFactory, _uiFactory);
            _saveLoadSystem = new PlayerPrefsSystem();
            _appleCounter = new AppleCounter(_saveLoadSystem);
            _appleHit = new AppleHit(_appleCounter, _gameFactory);
            _stagesCounter = new StagesCounter();
            _knivesCollection = new KnivesCollection(_gameFactory);
            _knivesCounter = new KnivesCounter(_gameFactory, _stagesCounter);
            _loseController = new LoseController(_gameFactory, _uiFactory, _knivesCollection, _stagesCounter, _knivesCounter);
            _victoryController = new VictoryController(_gameFactory, _knivesCounter, _knivesCollection, _stagesCounter);

            _gameFactory.Initialize(_loseController, _victoryController, _stagesCounter, _appleHit);
            _uiFactory.Initialize(_appleCounter, _knivesCounter, _victoryController, _restarterController, _gameFactory.StageConfig, _stagesCounter, _gameFactory);
            
            _uiFactory.CreateCanvas();
            _uiFactory.CreateHUD();
            
            _gameFactory.CreateContainer();
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
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