using UnityEngine;

namespace CodeBase.Behaviours
{
    public class Motion : MonoBehaviour
    {
        private GameObject _log;
        private Rigidbody2D _rb;
        private bool _rotation = false;
        private float _rotateSpeed;
        
        private readonly Vector3 _startPosition = new Vector3(0f, -3.5f, 0f);

        public void InitializeLog(GameObject log) => 
            _log = log;

        public void InitializeRigidbody2D(Rigidbody2D rb) => 
            _rb = rb;
        
        private void FixedUpdate()
        {
            if (_rotation) 
                Rotate();
        }

        public void MoveForward() => 
            _rb.velocity = transform.up * 15f;

        public void MoveBack() => 
            _rb.velocity = -transform.up * 5f;

        public void StopMove() =>
            _rb.velocity = Vector2.zero;

        public void StartRandomRotation()
        {
            var random = Random.Range(1, 20);
            _rotateSpeed = random;
            _rotation = true;
        }

        public void TurnOnGravity() => 
            _rb.gravityScale = 1f;

        public void SetPosition(Transform beam, float position) => 
            transform.RotateAround(beam.transform.position, Vector3.forward, position);

        public void SetPositionPlayerKnife() => 
            transform.position = _startPosition;

        private void Rotate() => 
            transform.Rotate(new Vector3(0f, 0f, _rotateSpeed));
    }
}