using CodeBase.Factories;

namespace CodeBase.Game
{
    public class GameStarter
    {
        private readonly GameFactory _gameFactory;
        private readonly UIFactory _uiFactory;

        public GameStarter(GameFactory gameFactory, UIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void CreateGameObjects()
        {
            _uiFactory.DestroyStartScreen();
            _uiFactory.CreateHUD();
            _gameFactory.CreateContainer();
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
        }
    }
}