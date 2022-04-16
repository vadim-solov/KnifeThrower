using CodeBase.Game.Controllers;
using CodeBase.Game.Hit;
using CodeBase.ObjectType;
using UnityEngine;

namespace CodeBase.Behaviours
{
    public class CollisionChecker : MonoBehaviour
    {
        private bool _enabled = true;
        private LoseController _loseController;
        private AppleHit _appleHit;
        private EnemyHit _enemyHit;

        public void Initialize(LoseController loseController, AppleHit appleHit, EnemyHit enemyHit)
        {
            _loseController = loseController;
            _appleHit = appleHit;
            _enemyHit = enemyHit;
        }

        public void SwitchOff() => 
            _enabled = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && _enabled)
            {
                if (collision.gameObject.TryGetComponent(out Enemy log)) 
                    _enemyHit.OnHitInLog(gameObject, log);

                if (collision.gameObject.TryGetComponent(out Apple apple)) 
                    _appleHit.OnHitInApple(gameObject, apple);

                if (collision.gameObject.TryGetComponent(out Knife knife)) 
                    _loseController.OnLose(gameObject, knife);
            }
        }
    }
}