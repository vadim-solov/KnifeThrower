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

        private Canvas _canvas;
        private RectTransform _loseScreen;
        private GameFactory _gameFactory;

        public void Initialize(GameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public void CreateCanvas() => 
            _canvas = Instantiate(_canvasPrefab);

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