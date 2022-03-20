using UnityEngine;

namespace CodeBase.Behaviours
{
    public class CollisionChecker : MonoBehaviour
    {
        [SerializeField]
        private Motion _knife;
        [SerializeField]
        private Attacher _attacher;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other != null)
            {
                _knife.StopMotion();
                var connectedBody = other.gameObject.GetComponent<Rigidbody>();
                _attacher.Attach(connectedBody);
                
            }
        }
    }
}