using System;
using System.Threading.Tasks;
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
        private const float AttachmentDepth = -0.9f;
        
        private readonly KnivesCounter _knivesCounter;
        private readonly GameFactory _gameFactory;
        private readonly ScoreCounter _scoreCounter;
        private readonly float _delayBetweenShots;
        
        public LogHit(KnivesCounter knivesCounter, GameFactory gameFactory, ScoreCounter scoreCounter, float delayBetweenShots)
        {
            _knivesCounter = knivesCounter;
            _gameFactory = gameFactory;
            _scoreCounter = scoreCounter;
            _delayBetweenShots = delayBetweenShots;
        }
        
        public void OnHitInLog(GameObject playerKnife, Log log)
        {
            LogMotion logMotion = log.GetComponent<LogMotion>();
            logMotion.StartShake();
            CreateHitParticles(playerKnife.transform.position);
            Motion motion = playerKnife.GetComponent<Motion>();
            motion.StopMove();
            _gameFactory.AddAttach(playerKnife, AttachmentDepth);
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
            
            CreatePlayerKnife();
        }

        private async void CreatePlayerKnife()
        {
            await Task.Delay(TimeSpan.FromSeconds(_delayBetweenShots));
            _gameFactory.CreatePlayerKnife();
        }
    }
}