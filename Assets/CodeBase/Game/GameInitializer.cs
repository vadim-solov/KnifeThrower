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
            _gameFactory.CreateBeam();
        }
    }
}