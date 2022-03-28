using CodeBase.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class GameRestarter : MonoBehaviour
    {
        [SerializeField] 
        private Button _restartButton;

        private GameFactory _gameFactory;
        private UIFactory _uiFactory;

        public void Initialize(GameFactory gameFactory, UIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        private void Start() => 
            _restartButton.onClick.AddListener(OnClick);

        private void OnDisable() => 
            _restartButton.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            _uiFactory.DestroyLoseScreen();
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
        }
    }
}