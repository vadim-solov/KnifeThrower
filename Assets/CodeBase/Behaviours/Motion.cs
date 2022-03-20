using UnityEngine;

namespace CodeBase.Behaviours
{
    public class Motion : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rb;
        [SerializeField, Range(0f,100f)]
        private float _speed = 50f;

        public void Initialize(Rigidbody rb, float speed)
        {
            _rb = rb;
            _speed = speed;
        }
        
        public void StartMotion() => 
            _rb.velocity = transform.up * _speed;

        public void StopMotion() => 
            _rb.velocity = Vector3.zero;
    }
}