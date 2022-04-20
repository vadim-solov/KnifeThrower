using CodeBase.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class PlayButton : MonoBehaviour
    {
        private const string SpawnKnifeAnimation = "SpawnKnife";

        [SerializeField]
        private Button _playButton;

        private IGameFactory _gameFactory;
        private IUIFactory _uiFactory;

        public void Initialize(IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _playButton.onClick.AddListener(Play);
        }

        private void OnDisable() => 
            _playButton.onClick.RemoveListener(Play);

        private void Play()
        {
            _uiFactory.DestroyUI(UIType.StartScreen);
            _uiFactory.CreateHUD();
            _gameFactory.CreateContainer();
            _gameFactory.CreateEnemy();
            _gameFactory.CreateApple();
            _gameFactory.TryCreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
            _gameFactory.PlayerKnife.GetComponent<Animator>().SetBool(SpawnKnifeAnimation, true);
        }
    }
}