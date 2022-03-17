using System.Collections.Generic;
using UnityEngine;
using System;

namespace CodeBase.Beam
{
    public class Motion : MonoBehaviour
    {
        [SerializeField, Range(-100f, 100f)]
        private float _rotateSpeed = 50f;
        [SerializeField, Range(0.1f, 3f)] 
        private float _disctance = 1f;

        private Transform _beam;
        private bool _rotate;

        private void Update()
        {
            if (_rotate) 
                RotateObject();
        }

        public void Initialize(Transform beam)
        {
            _beam = beam;
            transform.position = _beam.transform.position + new Vector3(_disctance, 0f, 0f);
        }

        public void SetAngle(float angle) => 
            gameObject.transform.RotateAround(_beam.transform.position, Vector3.forward, angle);

        public void StartRotation() =>
            _rotate = true;

        private void RotateObject() => 
            gameObject.transform.RotateAround(_beam.transform.position, Vector3.forward, _rotateSpeed * Time.deltaTime);
    }
}