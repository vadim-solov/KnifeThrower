using UnityEngine;

namespace CodeBase.Knife
{
    public class Attacher : MonoBehaviour
    {
        public void Attach(Rigidbody connectedBody)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = connectedBody.gameObject.GetComponent<Rigidbody>();
        }
    }
}