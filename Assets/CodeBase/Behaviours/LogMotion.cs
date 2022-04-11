using System.Collections;
using UnityEngine;

namespace CodeBase.Behaviours
{
    public class LogMotion : MonoBehaviour
    {
        private float _rotateSpeed = 50f;
        private float _rotationTime = 0f;
        private float _rotationStopTime = 0f;
        private bool _logRotation = false;

        public void LogInitialize(float rotateSpeed, float rotationTime, float rotationStopTime)
        {
            _rotateSpeed = rotateSpeed;
            _rotationTime = rotationTime;
            _rotationStopTime = rotationStopTime;
        }
        
        private void FixedUpdate()
        {
            if (_logRotation) 
                Rotate();
        }
        
        public void StartRotationTimer() => 
            StartCoroutine(StartRotationDuration());

        private IEnumerator StartRotationDuration()
        {
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
                y = Random.Range(-0.07f, 0.07f);
                transform.position = new Vector3(0, y, 0); 
                yield return new WaitForSeconds(0.05f);
            }

            transform.position = new Vector3(0f, 0f, 0f);
        }
        
        public void StopRotation() => 
            _logRotation = false;

        public void StartRotation() => 
            _logRotation = true;
    }
}