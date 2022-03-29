using CodeBase.Game;
using CodeBase.ObjectType;
using UnityEngine;

namespace CodeBase.Behaviours
{
    public class CollisionChecker : MonoBehaviour
    {
        private bool _enabled = true;

        private LoseController _loseController;
        private VictoryController _victoryController;
        private AppleHit _appleHit;

        public void Initialize(LoseController loseController, VictoryController victoryController, AppleHit appleHit)
        {
            _loseController = loseController;
            _victoryController = victoryController;
            _appleHit = appleHit;
        }

        public void SwitchOff() => 
            _enabled = false;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision != null && _enabled)
            {
                if (collision.gameObject.TryGetComponent(out Beam beam))
                {
                    Debug.Log("Hit in beam");
                    _victoryController.OnHitInBeam(gameObject, beam);
                }
                
                if (collision.gameObject.TryGetComponent(out Apple apple))
                {
                    Debug.Log("Hit in apple");
                    _appleHit.OnHitInApple(gameObject, apple);
                }     
                
                if (collision.gameObject.TryGetComponent(out Knife knife))
                {
                    Debug.Log("Hit in knife");
                    _loseController.OnLose(gameObject, knife);
                }
            }
        }
    }
}