using UnityEngine;

namespace CodeBase.Knife
{
    public class KnifeMotion : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rb;
        [SerializeField, Range(0f,100f)]
        private float _speed = 50f;

        public void StartMotion() => 
            _rb.velocity = transform.up * _speed;

        public void StopMotion() => 
            _rb.velocity = Vector3.zero;
    }
}