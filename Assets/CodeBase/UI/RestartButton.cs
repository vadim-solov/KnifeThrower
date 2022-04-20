using CodeBase.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class RestartButton : MonoBehaviour
    {
        private const string SpawnKnifeAnimation = "SpawnKnife";

        [SerializeField] 
        private Button _restartButton;

        private IGameFactory _gameFactory;
        private IUIFactory _uiFactory;

        public void Initialize(IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _restartButton.onClick.AddListener(OnClick);
        }

        private void OnDisable() => 
            _restartButton.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            _uiFactory.DestroyUI(UIType.LoseScreen);
            _uiFactory.ShowStage();
            _uiFactory.ShowScore();
            _gameFactory.CreateContainer();
            _gameFactory.CreateEnemy();
            _gameFactory.CreateApple();
            _gameFactory.TryCreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
            _gameFactory.PlayerKnife.GetComponent<Animator>().SetBool(SpawnKnifeAnimation, true);
        }
    }
}