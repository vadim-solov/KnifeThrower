using CodeBase.Behaviours;
using CodeBase.Factories;
using CodeBase.Game.Counters;
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
            Motion motion = playerKnife.GetComponent<Motion>();
            motion.StopMove();
            motion.StickItIn();
            motion.Attach(beam.gameObject);
            SwitchOffCollision(playerKnife);
            SwitchOffInput(playerKnife);
            _knivesCounter.Decrease();
            _scoreCounter.IncreaseScore();
            TryCreatePlayerKnife();
        }

        private void SwitchOffCollision(GameObject playerKnife) => 
            playerKnife.gameObject.GetComponent<CollisionChecker>().SwitchOff();

        private void SwitchOffInput(GameObject playerKnife) => 
            playerKnife.gameObject.GetComponent<KnifeInput>().enabled = false;

        private void TryCreatePlayerKnife()
        {
            if(_knivesCounter.CheckLastKnife())
                return;
            
            _gameFactory.CreatePlayerKnife();
        }
    }
}