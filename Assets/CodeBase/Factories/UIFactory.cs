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
        
        private Canvas _canvas;
        private RectTransform _loseScreen;
        private GameFactory _gameFactory;
        private RectTransform _HUD;
        private AppleCounter _appleCounter;
        private KnivesCounter _knivesCounter;
        private VictoryController _victoryController;

        public void Initialize(GameFactory gameFactory, AppleCounter appleCounter, KnivesCounter knivesCounter, VictoryController victoryController)
        {
            _gameFactory = gameFactory;
            _appleCounter = appleCounter;
            _knivesCounter = knivesCounter;
            _victoryController = victoryController;
        }

        public void CreateCanvas() => 
            _canvas = Instantiate(_canvasPrefab);

        public void CreateHUD()
        {
            _HUD = Instantiate(_HUDPrefab, _canvas.transform);
            _HUD.gameObject.GetComponent<AppleScoreHUD>().Initialize(_appleCounter);
            _HUD.gameObject.GetComponent<KnivesHUD>().Initialize(_knivesCounter, _victoryController);
        }

        public void CreateLoseScreen()
        {
            _loseScreen = Instantiate(_loseScreenPrefab, _canvas.transform);
            var restarter = _loseScreen.GetComponent<GameRestarter>();
            restarter.Initialize(_gameFactory, this);
        }

        public void DestroyLoseScreen() => 
            Destroy(_loseScreen.gameObject);
    }
}