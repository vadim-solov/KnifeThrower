using UnityEngine;

namespace CodeBase.Behaviours
{
    public class Motion : MonoBehaviour
    {
        private GameObject _log;
        private Rigidbody2D _rb;

        private readonly Vector3 _startPosition = new Vector3(0f, -3.5f, 0f);

        public void InitializeLog(GameObject log) => 
            _log = log;

        public void InitializeRigidbody2D(Rigidbody2D rb) => 
            _rb = rb;

        public void MoveForward() => 
            _rb.velocity = transform.up * 10f;

        public void StopMove() =>
            _rb.velocity = Vector2.zero;
        
        public void MoveBack() => 
            _rb.velocity = -transform.up * 5f;
        
        public void StartRandomRotation()
        {
            var random = Random.Range(1, 25);
            //_rotateSpeed = random; 
        }
        
        public void SetPosition(Transform beam, float position) => 
            transform.RotateAround(beam.transform.position, Vector3.forward, position);

        public void SetPositionPlayerKnife() => 
            transform.position = _startPosition;

        public void Drop()
        {
            
            _rb.AddForce(transform.up * 0.005f * Time.deltaTime);
        }
    }
}