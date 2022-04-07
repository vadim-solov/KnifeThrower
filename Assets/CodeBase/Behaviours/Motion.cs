using UnityEngine;

namespace CodeBase.Behaviours
{
    public class Motion : MonoBehaviour
    {
        private Rigidbody _rb;
        private float _rotateSpeed = 50f;
        private bool _rotate;

        public void Initialize(Rigidbody rb)
        {
            _rb = rb;
            _rb.mass = 0f;
            _rb.useGravity = false;
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

        public void RotateBeam() => 
            transform.rotation = new Quaternion(90f, 0f, 0f, 90f);

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