using UnityEngine;

namespace CodeBase.Behaviours
{
    public class Motion : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rb;
        [SerializeField, Range(0f,100f)]
        private float _speed = 50f;
        
        
        private float _rotateSpeed = 50f;
        private bool _rotate;

        public void Initialize(Rigidbody rb, float speed)
        {
            _rb = rb;
            _speed = speed;
        }

        private void FixedUpdate()
        {
            if (_rotate) 
                Rotation();
        }

        public void StartRotation(float rotateSpeed)
        {
            _rotateSpeed = rotateSpeed; 
            _rotate = true;
        }

        private void Rotation() => 
            transform.Rotate(new Vector3(0f, 0f, _rotateSpeed)); //Refactor this. Use rigidbody
        
        public void StartMotion() => 
            _rb.velocity = transform.up * _speed;

        public void StopMotion() => 
            _rb.velocity = Vector3.zero;

        public void Drop()
        {
            _rb.useGravity = true;
            _rb.AddForce(transform.up * 0.005f * Time.deltaTime);
        }
    }
}