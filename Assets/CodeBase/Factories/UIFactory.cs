using CodeBase.Game;
using CodeBase.HUD;
using CodeBase.SaveLoadSystem;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Factories
{
    [CreateAssetMenu]
    public class UIFactory : ScriptableObject
    {
        [SerializeField]
        private Canvas _canvasPrefab;
        [SerializeField]
        private RectTransform _loseScreenPrefab;
        [SerializeField]
        private RectTransform _HUDPrefab;
        [SerializeField]
        private GameObject _knifePrefab;
        [SerializeField]
        private RectTransform _startScreenPrefab;
        [SerializeField]
        private RectTransform _skinsScreenPrefab;

        private Canvas _canvas;
        private RectTransform _loseScreen;
        private RectTransform _hud;
        private AppleCounter _appleCounter;
        private KnivesCounter _knivesCounter;
        private RestarterController _restarterController;
        private StagesCounter _stagesCounter;
        private GameFactory _gameFactory;
        private ScoreCounter _scoreCounter;
        private RectTransform _startScreen;
        private GameStarter _gameStarter;
        private RectTransform _skinsScreen;
        private ISaveLoadSystem _saveLoadSystem;
        private Skins _skins;
        private Camera _camera;

        public void Initialize(AppleCounter appleCounter, KnivesCounter knivesCounter, RestarterController restarterController, 
            StagesCounter stagesCounter, GameFactory gameFactory, ScoreCounter scoreCounter, GameStarter gameStarter, ISaveLoadSystem saveLoadSystem, Skins skins, Camera cameraPrefab)
        {
            _appleCounter = appleCounter;
            _knivesCounter = knivesCounter;
            _restarterController = restarterController;
            _stagesCounter = stagesCounter;
            _gameFactory = gameFactory;
            _scoreCounter = scoreCounter;
            _gameStarter = gameStarter;
            _saveLoadSystem = saveLoadSystem;
            _skins = skins;
            _camera = cameraPrefab;
        }

        public void CreateCamera() => 
            _camera = Instantiate(_camera);

        public void CreateCanvas()
        {
            _canvas = Instantiate(_canvasPrefab);
            _canvas.GetComponent<Canvas>().worldCamera = _camera;
        }

        public void CreateHUD()
        {
            _hud = Instantiate(_HUDPrefab, _canvas.transform);
            _hud.GetComponent<AppleScoreHUD>().Initialize(_appleCounter);
            _hud.GetComponent<KnivesHUD>().Initialize(this, _knivesCounter, _gameFactory);
            _hud.GetComponent<StageHUD>().Initialize(_stagesCounter, _gameFactory.StageConfig);
            _hud.GetComponent<ScoreHUD>().Initialize(_scoreCounter);
        }

        public void CreateLoseScreen()
        {
            _loseScreen = Instantiate(_loseScreenPrefab, _canvas.transform);
            _loseScreen.GetComponent<RestartButton>().Initialize(_restarterController);
            _loseScreen.GetComponent<LevelStatistic>().Initialize(_scoreCounter, _stagesCounter);
        }

        public void DestroyLoseScreen() => 
            Destroy(_loseScreen.gameObject);

        public GameObject CreateKnife()
        {
            GameObject knife = Instantiate(_knifePrefab);
            return knife;
        }

        public void CreateStartScreen()
        {
            _startScreen = Instantiate(_startScreenPrefab, _canvas.transform);
            _startScreen.GetComponent<PlayButton>().Initialize(_gameStarter);
            _startScreen.GetComponent<Records>().Initialize(_saveLoadSystem);
            _startScreen.GetComponent<SkinsButton>().Initialize(this);
        }

        public void DestroyStartScreen() => 
            Destroy(_startScreen.gameObject);

        public void CreateSkinsScreen()
        {
            _skinsScreen = Instantiate(_skinsScreenPrefab, _canvas.transform);
            _skinsScreen.GetComponent<SkinsLoader>().Initialize(_skins, _gameFactory);
        }
    }
}