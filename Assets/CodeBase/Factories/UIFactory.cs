using CodeBase.Game;
using CodeBase.Game.Controllers;
using CodeBase.Game.Counters;
using CodeBase.SaveLoadSystem;
using CodeBase.UI;
using CodeBase.UI.HUD;
using UnityEngine;

namespace CodeBase.Factories
{
    [CreateAssetMenu]
    public class UIFactory : ScriptableObject, IUIFactory
    {
        [Header("Canvas")]
        [SerializeField]
        private Canvas _canvasPrefab;

        [Header("HUD")]
        [SerializeField]
        private RectTransform _HUDPrefab;
        [SerializeField]
        private GameObject _knifePrefab;
        [SerializeField]
        private RectTransform _appleScorePrefab;

        [Header("Screens")]
        [SerializeField]
        private RectTransform _loseScreenPrefab;
        [SerializeField]
        private RectTransform _startScreenPrefab;
        [SerializeField]
        private RectTransform _skinsScreenPrefab;
        [SerializeField]
        private RectTransform _maxStageScreenPrefab;
        [SerializeField]
        private RectTransform _newSkinsWindowPrefab;

        private Canvas _canvas;
        private RectTransform _loseScreen;
        private RectTransform _hud;
        private AppleCounter _appleCounter;
        private KnivesCounter _knivesCounter;
        private StagesCounter _stagesCounter;
        private ScoreCounter _scoreCounter;
        private RectTransform _startScreen;
        private RectTransform _skinsScreen;
        private Skins _skins;
        private Camera _camera;
        private RectTransform _maxStageScreen;
        private RectTransform _newSkinWindow;
        private VictoryController _victoryController;
        private RectTransform _appleScore;
        private StageHUD _stage;
        private ScoreHUD _score;

        private IGameFactory _gameFactory;
        private ISaveLoadSystem _saveLoadSystem;

        public void Initialize(AppleCounter appleCounter, KnivesCounter knivesCounter, StagesCounter stagesCounter, IGameFactory gameFactory, 
            ScoreCounter scoreCounter, ISaveLoadSystem saveLoadSystem, Skins skins, Camera cameraPrefab, VictoryController victoryController)
        {
            _victoryController = victoryController;
            _appleCounter = appleCounter;
            _knivesCounter = knivesCounter;
            _stagesCounter = stagesCounter;
            _gameFactory = gameFactory;
            _scoreCounter = scoreCounter;
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
            _hud.GetComponent<KnivesHUD>().Initialize(this, _knivesCounter, _gameFactory);
            _stage = _hud.GetComponent<StageHUD>();
            _stage.Initialize(_gameFactory, _stagesCounter, _victoryController);
            _score = _hud.GetComponent<ScoreHUD>();
            _score.Initialize(_scoreCounter);
            CreateAppleScore(_hud);
        }

        public void CreateLoseScreen()
        {
            _loseScreen = Instantiate(_loseScreenPrefab, _canvas.transform);
            _loseScreen.GetComponent<HomeButton>().Initialize(this);
            _loseScreen.GetComponent<RestartButton>().Initialize(_gameFactory, this);
            _loseScreen.GetComponent<LevelStatistic>().Initialize(_scoreCounter, _stagesCounter);
            CreateAppleScore(_loseScreen);
        }

        public void CreateStartScreen()
        {
            _startScreen = Instantiate(_startScreenPrefab, _canvas.transform);
            _startScreen.GetComponent<PlayButton>().Initialize(_gameFactory, this);
            _startScreen.GetComponent<Records>().Initialize(_saveLoadSystem);
            _startScreen.GetComponent<SkinsButton>().Initialize(this);
            CreateAppleScore(_startScreen);
        }

        public void CreateSkinsScreen()
        {
            _skinsScreen = Instantiate(_skinsScreenPrefab, _canvas.transform);
            _skinsScreen.GetComponent<BackButton>().Initialize(this);
            _skinsScreen.GetComponent<SkinsLoader>().Initialize(_skins, _stagesCounter);
            CreateAppleScore(_skinsScreen);
        }

        public void CreateMaxStageScreen() => 
            _maxStageScreen = Instantiate(_maxStageScreenPrefab, _canvas.transform);

        public GameObject CreateKnife()
        {
            GameObject knife = Instantiate(_knifePrefab);
            return knife;
        }

        public RectTransform CreatNewSkinWindow(Sprite sprite)
        {
            _newSkinWindow = Instantiate(_newSkinsWindowPrefab, _canvas.transform);
            _newSkinWindow.GetComponent<NewSkinWindowImage>().AddSprite(sprite);
            return _newSkinWindow;
        }

        public void DestroyUI(UIType type)
        {
            RectTransform entity = null;

            switch (type)
            {
                case UIType.NewSkinWindow:
                    entity = _newSkinWindow;
                    break;

                case UIType.HUD:
                    entity = _hud;
                    break;
                
                case UIType.LoseScreen:
                    entity = _loseScreen;
                    break;
                
                case UIType.StartScreen:
                    entity = _startScreen;
                    break;
                
                case UIType.SkinsScreen:
                    entity = _skinsScreen;
                    break;
            }

            if (entity != null)
                Destroy(entity.gameObject);
        }

        public void HideStage() =>
            _stage.StageText.gameObject.SetActive(false);

        public void HideScore() =>
            _score.ScoreText.gameObject.SetActive(false);
        
        public void ShowStage() =>
            _stage.StageText.gameObject.SetActive(true);

        public void ShowScore() =>
            _score.ScoreText.gameObject.SetActive(true);

        private void CreateAppleScore(RectTransform parent)
        {
            _appleScore = Instantiate(_appleScorePrefab, parent.transform);
            _appleScore.GetComponent<AppleScoreHUD>().Initialize(_appleCounter);
        }
    }
}