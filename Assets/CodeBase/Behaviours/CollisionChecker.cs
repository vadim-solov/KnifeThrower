using System;
using CodeBase.Game;
using CodeBase.Game.Controllers;
using CodeBase.ObjectType;
using UnityEngine;

namespace CodeBase.Behaviours
{
    public class CollisionChecker : MonoBehaviour
    {
        private bool _enabled = true;
        private LoseController _loseController;
        private AppleHit _appleHit;
        private LogHit _logHit;

        public void Initialize(LoseController loseController, AppleHit appleHit, LogHit logHit)
        {
            _loseController = loseController;
            _appleHit = appleHit;
            _logHit = logHit;
        }

        public void SwitchOff() => 
            _enabled = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && _enabled)
            {
                if (collision.gameObject.TryGetComponent(out Log log)) 
                    _logHit.OnHitInLog(gameObject, log);

                if (collision.gameObject.TryGetComponent(out Apple apple)) 
                    _appleHit.OnHitInApple(gameObject, apple);

                if (collision.gameObject.TryGetComponent(out Knife knife)) 
                    _loseController.OnLose(gameObject, knife);
            }
        }
    }
}