using CodeBase.Game;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;

        private GameStarter _gameStarter;

        public void Initialize(GameStarter gameStarter)
        {
            _gameStarter = gameStarter;
            _playButton.onClick.AddListener(Play);
        }

        private void OnDisable() => 
            _playButton.onClick.RemoveListener(Play);

        private void Play() => 
            _gameStarter.CreateGameObjects();
    }
}