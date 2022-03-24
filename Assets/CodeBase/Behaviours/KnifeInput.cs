using UnityEngine;

namespace CodeBase.Behaviours
{
    public class KnifeInput : MonoBehaviour
    {
        private Motion _motion;
        private float _speed;

        public void Initialize(Motion motion, float speed)
        {
            _motion = motion;
            _speed = speed;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
                _motion.MoveForward(_speed);
        }
    }
}