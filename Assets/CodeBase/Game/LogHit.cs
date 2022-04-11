using CodeBase.Behaviours;
using CodeBase.Factories;
using CodeBase.Game.Counters;
using CodeBase.ObjectType;
using UnityEngine;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Game
{
    public class LogHit
    {
        private readonly KnivesCounter _knivesCounter;
        private readonly GameFactory _gameFactory;
        private readonly ScoreCounter _scoreCounter;
        
        public LogHit(KnivesCounter knivesCounter, GameFactory gameFactory, ScoreCounter scoreCounter)
        {
            _knivesCounter = knivesCounter;
            _gameFactory = gameFactory;
            _scoreCounter = scoreCounter;
        }
        
        public void OnHitInLog(GameObject playerKnife, Log log)
        {
            LogMotion logMotion = log.GetComponent<LogMotion>();
            logMotion.StartShake();
            CreateHitParticles(playerKnife.transform.position);
            Motion motion = playerKnife.GetComponent<Motion>();
            motion.StopMove();
            _gameFactory.AddAttach(playerKnife, -0.3f);
            SwitchOffCollision(playerKnife);
            SwitchOffInput(playerKnife);
            _knivesCounter.Decrease();
            _scoreCounter.IncreaseScore();
            TryCreatePlayerKnife();
        }

        private void CreateHitParticles(Vector3 position) => 
            _gameFactory.CreateParticlesOnImpact(position);

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