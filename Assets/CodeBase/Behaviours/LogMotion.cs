using System.Collections;
using UnityEngine;

namespace CodeBase.Behaviours
{
    public class LogMotion : MonoBehaviour
    {
        private float _rotateSpeed = 50f;
        private float _rotationTime = 0f;
        private float _rotationStopTime = 0f;
        private bool _rotation = false;
        
        private readonly Vector3 _startPosition = new Vector3(0f, 1f, 0f);

        public void LogInitialize(float rotateSpeed, float rotationTime, float rotationStopTime)
        {
            _rotateSpeed = rotateSpeed;
            _rotationTime = rotationTime;
            _rotationStopTime = rotationStopTime;
        }
        
        private void FixedUpdate()
        {
            if (_rotation) 
                Rotate();
        }
        
        public void StartRotationTimer() => 
            StartCoroutine(StartRotationDuration());

        private IEnumerator StartRotationDuration()
        {
            if (!_rotation)
                yield break;
            
            yield return new WaitForSeconds(_rotationTime);
            StopRotation();
            StartCoroutine(StartRotationStopTimer());
        }

        private IEnumerator StartRotationStopTimer()
        {
            yield return new WaitForSeconds(_rotationStopTime);
            StartRotation();
            StartCoroutine(StartRotationDuration());
        }
        
        private void Rotate() => 
            transform.Rotate(new Vector3(0f, 0f, _rotateSpeed));
        
        public void StartShake() => 
            StartCoroutine(Shake());

        private IEnumerator Shake()
        {
            float y;
            float timeLeft = Time.time;

            while ((timeLeft + 0.1f) > Time.time)
            {
                y = Random.Range(1.0f, 1.07f);
                transform.position = new Vector3(0, y, 0); 
                yield return new WaitForSeconds(0.05f);
            }
            
            transform.position = _startPosition;
        }
        
        public void StopRotation() => 
            _rotation = false;

        public void StartRotation() => 
            _rotation = true;

        public void SetPosition() => 
            transform.position = _startPosition;
    }
}