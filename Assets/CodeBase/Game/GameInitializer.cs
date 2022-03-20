using CodeBase.Behaviours;
using CodeBase.Factories;
using UnityEngine;

namespace CodeBase.Game
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameFactory _gameFactory;

        [SerializeField]
        private HitController _hitController;
        
        private void Awake()
        {
            _gameFactory.Initialize(_hitController);
            
            _gameFactory.CreateContainer();
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateKnives();
            _gameFactory.CreatePlayerKnife();
        }
    }
}