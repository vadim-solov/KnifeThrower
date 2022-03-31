using CodeBase.Game;
using CodeBase.HUD;
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

        private Canvas _canvas;
        private RectTransform _loseScreen;
        private RectTransform _hud;
        private AppleCounter _appleCounter;
        private KnivesCounter _knivesCounter;
        private VictoryController _victoryController;
        private RestarterController _restarterController;
        private StageConfig[] _stageConfig;
        private StagesCounter _stagesCounter;
        private GameFactory _gameFactory;

        public void Initialize(AppleCounter appleCounter, KnivesCounter knivesCounter, VictoryController victoryController, RestarterController restarterController, 
            StageConfig[] stageConfig, StagesCounter stagesCounter, GameFactory gameFactory)
        {
            _appleCounter = appleCounter;
            _knivesCounter = knivesCounter;
            _victoryController = victoryController;
            _restarterController = restarterController;
            _stageConfig = stageConfig;
            _stagesCounter = stagesCounter;
            _gameFactory = gameFactory;
        }

        public void CreateCanvas() => 
            _canvas = Instantiate(_canvasPrefab);

        public void CreateHUD()
        {
            _hud = Instantiate(_HUDPrefab, _canvas.transform);
            _hud.gameObject.GetComponent<AppleScoreHUD>().Initialize(_appleCounter);
            _hud.gameObject.GetComponent<KnivesHUD>().Initialize(this, _knivesCounter, _victoryController, _restarterController, _gameFactory);
            _hud.gameObject.GetComponent<StageHUD>().Initialize(_stageConfig, _stagesCounter, _gameFactory);
        }

        public void CreateLoseScreen()
        {
            _loseScreen = Instantiate(_loseScreenPrefab, _canvas.transform);
            var restarter = _loseScreen.GetComponent<RestartButton>();
            restarter.Initialize(_restarterController);
        }

        public void DestroyLoseScreen() => 
            Destroy(_loseScreen.gameObject);

        public GameObject CreateKnife()
        {
            GameObject knife = Instantiate(_knifePrefab);
            return knife;
        }
    }
}