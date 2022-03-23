using System;
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
        private KnivesCounter _knivesCounter;

        public event Action Lose;
            
        public void Initialize(IGameFactory gameFactory, KnivesCounter knivesCounter)
        {
            _gameFactory = gameFactory;
            _knivesCounter = knivesCounter;
        }

        public void HitInBeam(GameObject knife, Beam component)
        {            
            Debug.Log("Hit in beam");
            var motion = knife.GetComponent<Motion>();
            motion.StopMotion();
            FixedJoint joint = knife.AddComponent<FixedJoint>();
            joint.connectedBody = component.gameObject.GetComponent<Rigidbody>();
            knife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            knife.gameObject.GetComponent<KnifeInput>().enabled = false;
            _knivesCounter.Decrease();


            TryCreatePlayerKnife();
        }

        public void HitInApple(GameObject knife, Apple component)
        {
            Debug.Log("Hit in apple");
            
            var motion = knife.GetComponent<Motion>();
            motion.StopMotion();
            
            FixedJoint joint = knife.AddComponent<FixedJoint>();
            joint.connectedBody = component.gameObject.GetComponent<Rigidbody>();
            
            knife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            knife.gameObject.GetComponent<KnifeInput>().enabled = false;
        }

        public void HitInKnife(GameObject knife, Knife component)
        {
            Debug.Log("Hit in Knife");
            
            var motion = knife.GetComponent<Motion>();
            motion.StopMotion();
            
            FixedJoint joint = knife.AddComponent<FixedJoint>();
            joint.connectedBody = component.gameObject.GetComponent<Rigidbody>();
            
            knife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            knife.gameObject.GetComponent<KnifeInput>().enabled = false;
            
            Lose?.Invoke();
        }

        private void TryCreatePlayerKnife()
        {
            if(_knivesCounter.CheckLastKnife())
                return;
            
            _gameFactory.CreatePlayerKnife();
        }
    }
}