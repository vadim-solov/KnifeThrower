using System;
using System.Threading.Tasks;
using CodeBase.Behaviours;
using CodeBase.Factories;
using CodeBase.Game.Controllers;
using CodeBase.Game.Counters;
using CodeBase.ObjectType;
using CodeBase.Vibration;
using UnityEngine;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Game.Hit
{
    public class EnemyHit
    {
        private const float AttachmentDepth = -0.635f;
        private const string AnimationName = "SpawnKnife";

        private readonly KnivesCounter _knivesCounter;
        private readonly GameFactory _gameFactory;
        private readonly ScoreCounter _scoreCounter;
        private readonly VictoryController _victoryController;
        private readonly float _delayBetweenShots;

        public EnemyHit(KnivesCounter knivesCounter, GameFactory gameFactory, ScoreCounter scoreCounter, VictoryController victoryController, float delayBetweenShots)
        {
            _knivesCounter = knivesCounter;
            _gameFactory = gameFactory;
            _scoreCounter = scoreCounter;
            _victoryController = victoryController;
            _delayBetweenShots = delayBetweenShots;
        }
        
        public void OnHitInLog(GameObject playerKnife, Enemy enemy)
        {
            EnemyMotion enemyMotion = enemy.GetComponent<EnemyMotion>();
            enemyMotion.StartShake();
            CreateHitParticles(playerKnife.transform.position);
            Motion motion = playerKnife.GetComponent<Motion>();
            motion.StopMove();
            _gameFactory.AddAttach(playerKnife, AttachmentDepth);
            SwitchOffCollision(playerKnife);
            SwitchOffInput(playerKnife);
            _knivesCounter.Decrease();
            _scoreCounter.IncreaseScore();
            TryCreatePlayerKnife();
            MainVibration.Vibrate(50);
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
            
            if(_victoryController.IsVictory)
                return;
            
            CreatePlayerKnife();
        }

        private async void CreatePlayerKnife()
        {
            await Task.Delay(TimeSpan.FromSeconds(_delayBetweenShots));
            _gameFactory.CreatePlayerKnife();
            _gameFactory.PlayerKnife.GetComponent<Animator>().SetBool(AnimationName, true);
        }
    }
}