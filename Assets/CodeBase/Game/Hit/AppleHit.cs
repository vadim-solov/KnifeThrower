using CodeBase.Factories;
using CodeBase.Game.Counters;
using CodeBase.ObjectType;
using UnityEngine;

namespace CodeBase.Game.Hit
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
            SwitchOffCircleCollider(apple);
            _gameFactory.DestroyApple(0f);
            _gameFactory.CreateAppleParticles(apple.transform.position);
        }

        private void AddScore() => 
            _appleCounter.IncreaseScore();

        private void SwitchOffCircleCollider(Apple apple) => 
            apple.GetComponent<CircleCollider2D>().enabled = false;
    }
}