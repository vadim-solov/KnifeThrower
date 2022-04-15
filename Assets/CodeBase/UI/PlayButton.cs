using CodeBase.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;

        private GameFactory _gameFactory;
        private UIFactory _uiFactory;

        public void Initialize(GameFactory gameFactory, UIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _playButton.onClick.AddListener(Play);
        }

        private void OnDisable() => 
            _playButton.onClick.RemoveListener(Play);

        private void Play()
        {
            _uiFactory.DestroyStartScreen();
            _uiFactory.CreateHUD();
            _gameFactory.CreateContainer();
            _gameFactory.CreateLog();
            _gameFactory.CreateApple();
            _gameFactory.CreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
            _gameFactory.PlayerKnife.GetComponent<Animator>().SetBool("SpawnKnife", true);
        }
    }
}