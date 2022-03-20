using CodeBase.AttachedKnife;
using CodeBase.PinApple;
using UnityEngine;

namespace CodeBase.Behaviours
{
    public class CollisionChecker : MonoBehaviour
    {
        private HitController _hitController;

        public void Initialize(HitController hitController)
        {
            _hitController = hitController;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision != null)
            {
                if (collision.gameObject.TryGetComponent(out Beam.Beam beam))
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