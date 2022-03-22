using UnityEngine;

namespace CodeBase.Factories
{
    [CreateAssetMenu]
    public class UIFactory : ScriptableObject
    {
        [SerializeField]
        private Canvas _canvas;
        [SerializeField]
        private RectTransform _victoryScreen;

        public void CreateCanvas() => 
            Instantiate(_canvas);
    }
}