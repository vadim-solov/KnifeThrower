using System.Collections;
using UnityEngine;

namespace CodeBase.Behaviours
{
    public class EnemyMotion : MonoBehaviour
    {
        private const float ShakingShearDistance = 1.05f;
        
        private Vector3 _startPosition;
        private float _rotateSpeed;
        private float _currentRotateSpeed;
        private float _rotationTime;
        private float _rotationStopTime;
        private float _startStopImpulse;
        private bool _rotation = false;

        public void EnemyInitialize(Vector3 startPosition, float rotateSpeed, float rotationTime, float rotationStopTime, float startStopImpulse)
        {
            _startPosition = startPosition;
            _rotateSpeed = rotateSpeed;
            _currentRotateSpeed = rotateSpeed;
            _rotationTime = rotationTime;
            _rotationStopTime = rotationStopTime;
            _startStopImpulse = startStopImpulse;
        }
        
        private void FixedUpdate()
        {
            if (_rotation) 
                Rotate();
        }

        public void StartRotation()
        {
            _rotation = true;

            if (_rotationTime == 0f || _rotationStopTime == 0f)
                return;

            StartCoroutine(StartRotationDuration());
        }

        public void StopRotation()
        {
            _rotation = false;
            StopCoroutine(StartRotationDuration());
            StopCoroutine(SmoothStop());
            StopCoroutine(SmoothStart());
            StopCoroutine(StartRotationStopTimer());
        }

        public void SetStartPosition() => 
            transform.position = _startPosition;

        public void StartShake() => 
            StartCoroutine(Shake());

        private IEnumerator StartRotationDuration()
        {
            yield return new WaitForSeconds(_rotationTime);
            StartCoroutine(SmoothStop());
        }

        private IEnumerator SmoothStop()
        {
            while (_currentRotateSpeed >= 0)
            {
                _currentRotateSpeed -= Time.deltaTime * _startStopImpulse;
                yield return null;
            }

            _currentRotateSpeed = 0;
            StartCoroutine(StartRotationStopTimer());
        }

        private IEnumerator StartRotationStopTimer()
        {
            yield return new WaitForSeconds(_rotationStopTime);
            StartCoroutine(SmoothStart());
        }

        private IEnumerator SmoothStart()
        {
            while (_currentRotateSpeed <= _rotateSpeed)
            {
                _currentRotateSpeed += Time.deltaTime * _startStopImpulse;
                yield return null;
            }

            _currentRotateSpeed = _rotateSpeed;
            StartCoroutine(StartRotationDuration());
        }

        private IEnumerator Shake()
        {
            float timeLeft = Time.time;

            while ((timeLeft + 0.1f) > Time.time)
            {
                transform.position = new Vector3(0, ShakingShearDistance, 0); 
                yield return new WaitForSeconds(0.05f);
            }
            
            transform.position = _startPosition;
        }

        private void Rotate() => 
            transform.Rotate(new Vector3(0f, 0f, _currentRotateSpeed));
    }
}