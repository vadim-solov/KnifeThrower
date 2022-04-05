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
        private BeamHit _beamHit;

        public void Initialize(LoseController loseController, AppleHit appleHit, BeamHit beamHit)
        {
            _loseController = loseController;
            _appleHit = appleHit;
            _beamHit = beamHit;
        }

        public void SwitchOff() => 
            _enabled = false;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision != null && _enabled)
            {
                if (collision.gameObject.TryGetComponent(out Beam beam)) 
                    _beamHit.OnHitInBeam(gameObject, beam);

                if (collision.gameObject.TryGetComponent(out Apple apple)) 
                    _appleHit.OnHitInApple(gameObject, apple);

                if (collision.gameObject.TryGetComponent(out Knife knife)) 
                    _loseController.OnLose(gameObject, knife);
            }
        }
    }
}