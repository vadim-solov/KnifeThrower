using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Behaviours
{
    public class Motion : MonoBehaviour
    {
        private Rigidbody _rb;
        private bool _rotate;
        private float _rotateSpeed = 50f;
        private float _rotationTime;
        private float _rotationStopTime;

        public void Initialize(Rigidbody rb)
        {
            _rb = rb;
            _rb.mass = 0f;
            _rb.useGravity = false;
        }

        public void InitializeRotationTime(float rotationTime, float rotationStopTime)
        {
            _rotationTime = rotationTime;
            _rotationStopTime = rotationStopTime;
            
            if(_rotationTime == 0f)
                return;
            
            StartCoroutine(StartRotationTimer());
        }

        private void FixedUpdate()
        {
            if (_rotate) 
                Rotation();
        }

        private IEnumerator StartRotationTimer()
        {
            yield return new WaitForSeconds(_rotationTime);
            StopRotation();
            StartCoroutine(StartRotationStopTimer());
        } 

        private IEnumerator StartRotationStopTimer()
        {
            yield return new WaitForSeconds(_rotationStopTime);
            StartRotation(_rotateSpeed);
            StartCoroutine(StartRotationTimer());
        }

        public void StartRotation(float rotateSpeed)
        {
            _rotateSpeed = rotateSpeed; 
            _rotate = true;
        }

        public void StartRandomRotation()
        {
            var random = Random.Range(1, 25);
            _rotateSpeed = random; 
            _rotate = true;
        }

        public void StopRotation() => 
            _rotate = false;

        public void MoveForward(float speed) => 
            _rb.velocity = transform.up * speed;

        public void StopMove() => 
            _rb.velocity = Vector3.zero;

        public void Attach(GameObject beam)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = beam.GetComponent<Rigidbody>();
        }

        public void Detach(GameObject entity) //move to Motion
        {
            var attach = entity.GetComponent<FixedJoint>();
            Destroy(attach);
        }

        public void Drop()
        {
            _rb.useGravity = true;
            _rb.AddForce(transform.up * 0.005f * Time.deltaTime);
        }

        public void StartShake() => 
            StartCoroutine(_Shake());

        private IEnumerator _Shake()
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
        
        public void IsKinematic() => 
            _rb.isKinematic = true;

        public void FreezePosition() => 
            _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        public void SetPlayerKnife() => 
            transform.position = new Vector3(0f, -4f, 0f);

        public void SetDepth(Transform beam) => 
            transform.position = beam.transform.position + new Vector3(0.7f, 0f, 0f);

        public void SetPosition(Transform beam, float position) => 
            transform.RotateAround(beam.transform.position, Vector3.forward, position);

        public void Rotate() => 
            transform.Rotate(0f, 0f, 90f);

        public void MoveBack() => 
            _rb.velocity = -transform.up * 5f;

        public void StickItIn() => 
            transform.position += new Vector3(0f, 0.2f, 0f); // FIX

        private void Rotation() => 
            transform.Rotate(new Vector3(0f, 0f, _rotateSpeed)); //Refactor this. Use rigidbody
    }
}