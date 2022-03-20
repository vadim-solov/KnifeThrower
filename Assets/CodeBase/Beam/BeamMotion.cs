using UnityEngine;

namespace CodeBase.Beam
{
    public class BeamMotion : MonoBehaviour
    {
        [SerializeField, Range(-100f, 100f)]
        private float _rotateSpeed = 50f;

        private bool _rotate;

        private void FixedUpdate()
        {
            if (_rotate) 
                RotateBeam();
        }

        public void StartRotation() =>
            _rotate = true;

        private void RotateBeam() => 
            transform.Rotate(new Vector3(0f, 0f, _rotateSpeed)); //Refactor this. Use rigidbody
    }
}