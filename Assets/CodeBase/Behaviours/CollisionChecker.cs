using CodeBase.Game;
using CodeBase.ObjectType;
using UnityEngine;

namespace CodeBase.Behaviours
{
    public class CollisionChecker : MonoBehaviour
    {
        private bool _enabled = true;
        private HitController _hitController;

        public void Initialize(HitController hitController) => 
            _hitController = hitController;

        public void SwitchOff() => 
            _enabled = false;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision != null && _enabled)
            {
                if (collision.gameObject.TryGetComponent(out ObjectType.Beam beam))
                {
                    _hitController.HitInBeam(gameObject, beam);
                }
                
                if (collision.gameObject.TryGetComponent(out Apple apple))
                {
                    _hitController.HitInApple(gameObject, apple);
                }     
                
                if (collision.gameObject.TryGetComponent(out Knife knife))
                {
                    _hitController.HitInKnife(gameObject, knife);
                }
            }
        }
    }
}