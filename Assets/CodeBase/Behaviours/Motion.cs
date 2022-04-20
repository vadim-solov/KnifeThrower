using UnityEngine;

namespace CodeBase.Behaviours
{
    public class Motion : MonoBehaviour
    {
        private const float ForwardSpeed = 15f;
        private const float BackSpeed = 5f;
        private bool _rotation = false;
        private float _rotateSpeed;
        private Rigidbody2D _rb;

        public void Initialize(Rigidbody2D rb) => 
            _rb = rb;
        
        private void Update()
        {
            if (_rotation) 
                Rotate();
        }

        public void MoveForward() => 
            _rb.AddForce(transform.up * ForwardSpeed, ForceMode2D.Impulse);

        public void MoveBack() => 
            _rb.AddForce(-transform.up * BackSpeed, ForceMode2D.Impulse);

        public void StopMove() =>
            _rb.velocity = Vector2.zero;

        public void StartRandomRotation()
        {
            var random = Random.Range(100, 2000);
            _rotateSpeed = random;
            _rotation = true;
        }

        public void TurnOnGravity() => 
            _rb.gravityScale = 1f;

        public void SetStartPositionAroundEnemy(Vector3 enemyPosition, float position) => 
            transform.RotateAround(enemyPosition, Vector3.forward, position);

        private void Rotate() => 
            transform.Rotate(new Vector3(0f, 0f, _rotateSpeed) * Time.deltaTime);
        
        public void SetStartPositionPlayerKnife(Vector3 startPosition) => 
            transform.position = startPosition;
    }
}