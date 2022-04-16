using UnityEngine;

namespace CodeBase.Behaviours
{
    public class Motion : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private bool _rotation = false;
        private float _rotateSpeed;
        
        public void Initialize(Rigidbody2D rb) => 
            _rb = rb;
        
        private void FixedUpdate()
        {
            if (_rotation) 
                Rotate();
        }

        public void MoveForward() => 
            _rb.AddForce(transform.up * 15f, ForceMode2D.Impulse);

        public void MoveBack() => 
            _rb.AddForce(-transform.up * 5f, ForceMode2D.Impulse);

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

        public void SetStartPositionAroundEnemy(Vector3 enemyPosition, float position) => 
            transform.RotateAround(enemyPosition, Vector3.forward, position);

        private void Rotate() => 
            transform.Rotate(new Vector3(0f, 0f, _rotateSpeed));
        
        public void SetStartPositionPlayerKnife(Vector3 startPosition) => 
            transform.position = startPosition;
    }
}