using System;
using CodeBase.Factories;

namespace CodeBase.Game
{
    public class RestarterController
    {
        private readonly GameFactory _gameFactory;
        private readonly UIFactory _uiFactory;

        public event Action RestartEvent;

        public RestarterController(GameFactory gameFactory, UIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Restart()
        {
            _uiFactory.DestroyLoseScreen();
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
            RestartEvent?.Invoke();
        }
    }
}