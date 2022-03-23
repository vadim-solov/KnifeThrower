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
        
        public void CreateCanvas() => 
            _canvas = Instantiate(_canvasPrefab);

        public void CreateLoseScreen() => 
            _loseScreen = Instantiate(_loseScreenPrefab, _canvas.transform);
    }
}