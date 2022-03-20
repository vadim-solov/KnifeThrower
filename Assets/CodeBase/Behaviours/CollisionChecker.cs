using CodeBase.Beam;
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


                if (other.gameObject.TryGetComponent(out BeamMotion beam))
                {
                    Debug.Log("Hit in beam");
                    _knife.StopMotion();
                    var connectedBody1 = other.gameObject.GetComponent<Rigidbody>();
                    _attacher.Attach(connectedBody1);
                }
                
                if (other.gameObject.TryGetComponent(out PinBehaviour pin))
                {
                    Debug.Log("Hit in pin object");
                    _knife.StopMotion();
                    var connectedBody = other.gameObject.GetComponent<Rigidbody>();
                    _attacher.Attach(connectedBody);
                }
                
            }
        }
    }
}