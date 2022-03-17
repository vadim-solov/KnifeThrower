using CodeBase.Factories;
using UnityEngine;

namespace CodeBase.Game
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameFactory _gameFactory;
        
        private void Awake()
        {
            _gameFactory.CreateContainer();
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateKnives();
        }
    }
}