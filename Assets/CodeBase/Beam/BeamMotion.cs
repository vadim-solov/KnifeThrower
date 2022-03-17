using System;
using UnityEngine;

namespace CodeBase.Beam
{
    public class BeamMotion : MonoBehaviour
    {
        [SerializeField, Range(1, 100)]
        private float _rotateSpeed;

        private bool _rotate;

        private void Update()
        {
            if (_rotate) 
                RotateBeam();
        }

        public void StartRotation() =>
            _rotate = true;

        private void RotateBeam() => 
            transform.Rotate(new Vector3(0f, 0f, _rotateSpeed) * Time.deltaTime);
    }
}