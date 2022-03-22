using CodeBase.Behaviours;
using CodeBase.Factories;
using CodeBase.ObjectType;
using UnityEngine;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Game
{
    public class HitController : MonoBehaviour
    {
        private IGameFactory _gameFactory; 
            
        public void Initialize(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public void HitInBeam(GameObject knife, ObjectType.Beam component)
        {            
            Debug.Log("Hit in beam");
            var motion = knife.GetComponent<Motion>();
            motion.StopMotion();
            FixedJoint joint = knife.AddComponent<FixedJoint>();
            joint.connectedBody = component.gameObject.GetComponent<Rigidbody>();
            knife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            knife.gameObject.GetComponent<KnifeInput>().enabled = false;
            _gameFactory.CreatePlayerKnife();
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