using CodeBase.Factories;
using CodeBase.Game;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] 
        private Button _restartButton;

        private GameFactory _gameFactory;
        private UIFactory _uiFactory;

        public void Initialize(GameFactory gameFactory, UIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _restartButton.onClick.AddListener(OnClick);
        }

        private void OnDisable() => 
            _restartButton.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            _uiFactory.DestroyLoseScreen();
            _gameFactory.CreateContainer();
            _gameFactory.CreateEnemy();
            _gameFactory.CreateApple();
            _gameFactory.CreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
            _gameFactory.PlayerKnife.GetComponent<Animator>().SetBool("SpawnKnife", true);
        }
    }
}