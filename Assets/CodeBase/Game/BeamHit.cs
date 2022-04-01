using CodeBase.Behaviours;
using CodeBase.Factories;
using CodeBase.ObjectType;
using UnityEngine;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Game
{
    public class BeamHit
    {
        private readonly KnivesCounter _knivesCounter;
        private readonly GameFactory _gameFactory;
        private readonly ScoreCounter _scoreCounter;
        
        public BeamHit(KnivesCounter knivesCounter, GameFactory gameFactory, ScoreCounter scoreCounter)
        {
            _knivesCounter = knivesCounter;
            _gameFactory = gameFactory;
            _scoreCounter = scoreCounter;
        }
        
        public void OnHitInBeam(GameObject playerKnife, Beam beam)
        {
            var motion = playerKnife.GetComponent<Motion>();
            motion.StopMove();
            playerKnife.transform.position += new Vector3(0f, 0.2f, 0f); // FIX
            FixedJoint joint = playerKnife.AddComponent<FixedJoint>();
            joint.connectedBody = beam.gameObject.GetComponent<Rigidbody>();
            playerKnife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            playerKnife.gameObject.GetComponent<KnifeInput>().enabled = false;
            _knivesCounter.Decrease();
            _scoreCounter.IncreaseScore();
            TryCreatePlayerKnife();
        }
        
        private void TryCreatePlayerKnife()
        {
            if(_knivesCounter.CheckLastKnife())
                return;
            
            _gameFactory.CreatePlayerKnife();
        }
    }
}