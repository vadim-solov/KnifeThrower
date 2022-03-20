using CodeBase.AttachedKnife;
using CodeBase.PinApple;
using UnityEngine;

namespace CodeBase.Behaviours
{
    public class HitController : MonoBehaviour
    {
        public void HitInBeam(GameObject knife, Beam.Beam component)
        {            
            Debug.Log("Hit in beam");

            var motion = knife.GetComponent<Motion>();
            motion.StopMotion();
            
            FixedJoint joint = knife.AddComponent<FixedJoint>();
            joint.connectedBody = component.gameObject.GetComponent<Rigidbody>();
        }

        public void HitInApple(GameObject knife, Apple component)
        {
            Debug.Log("Hit in apple");
            
            var motion = knife.GetComponent<Motion>();
            motion.StopMotion();
            
            FixedJoint joint = knife.AddComponent<FixedJoint>();
            joint.connectedBody = component.gameObject.GetComponent<Rigidbody>();
        }

        public void HitInKnife(GameObject knife, Knife component)
        {
            Debug.Log("Hit in Knife");
            
            var motion = knife.GetComponent<Motion>();
            motion.StopMotion();
            
            FixedJoint joint = knife.AddComponent<FixedJoint>();
            joint.connectedBody = component.gameObject.GetComponent<Rigidbody>();
        }
    }
}