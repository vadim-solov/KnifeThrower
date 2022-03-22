using UnityEngine;

namespace CodeBase.Behaviours
{
    public class BeamMotion : MonoBehaviour
    {
        private float _rotateSpeed = 50f;
        private bool _rotate;

        private void FixedUpdate()
        {
            if (_rotate) 
                RotateBeam();
        }

        public void StartRotation(float rotateSpeed)
        {
            _rotateSpeed = rotateSpeed; 
            _rotate = true;
        }

        private void RotateBeam() => 
            transform.Rotate(new Vector3(0f, 0f, _rotateSpeed)); //Refactor this. Use rigidbody
    }
}