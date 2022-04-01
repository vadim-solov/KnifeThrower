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
        private BeamHit _beamHit;
        private ScoreCounter _scoreCounter;
        private GameStarter _gameStarter;

        private ISaveLoadSystem _saveLoadSystem;
        
        private void Awake()
        {
            _saveLoadSystem = new PlayerPrefsSystem();
            _stagesCounter = new StagesCounter(_saveLoadSystem);
            _gameStarter = new GameStarter(_gameFactory, _uiFactory);
            _restarterController = new RestarterController(_gameFactory, _uiFactory);
            _scoreCounter = new ScoreCounter(_saveLoadSystem);
            _appleCounter = new AppleCounter(_saveLoadSystem);
            _appleHit = new AppleHit(_appleCounter, _gameFactory);
            _knivesCollection = new KnivesCollection(_gameFactory);
            _knivesCounter = new KnivesCounter(_gameFactory, _stagesCounter);
            _loseController = new LoseController(_gameFactory, _uiFactory, _knivesCollection, _stagesCounter, _knivesCounter, _scoreCounter);
            _victoryController = new VictoryController(_gameFactory, _knivesCounter, _knivesCollection, _stagesCounter);
            _beamHit = new BeamHit(_knivesCounter, _gameFactory, _scoreCounter);

            _gameFactory.Initialize(_loseController, _stagesCounter, _appleHit, _beamHit);
            _uiFactory.Initialize(_appleCounter, _knivesCounter, _restarterController, _stagesCounter, _gameFactory, _scoreCounter, _gameStarter, _saveLoadSystem);
            
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