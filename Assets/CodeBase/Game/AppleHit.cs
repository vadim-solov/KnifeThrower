using CodeBase.Factories;
using CodeBase.Game.Counters;
using CodeBase.ObjectType;
using UnityEngine;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Game
{
    public class AppleHit
    {
        private readonly AppleCounter _appleCounter;
        private readonly GameFactory _gameFactory;

        public AppleHit(AppleCounter appleCounter, GameFactory gameFactory)
        {
            _appleCounter = appleCounter;
            _gameFactory = gameFactory;
        }

        public void OnHitInApple(GameObject playerKnife, Apple apple)
        {
            AddScore();
            SwitchOffBoxCollider(apple);
            Motion motion = apple.GetComponent<Motion>();
            motion.Detach(apple.gameObject);
            motion.Drop();
            _gameFactory.DestroyApple(2f);
        }

        private void AddScore() => 
            _appleCounter.IncreaseScore();

        private void SwitchOffBoxCollider(Apple apple) => 
            apple.GetComponent<BoxCollider>().enabled = false;
    }
}