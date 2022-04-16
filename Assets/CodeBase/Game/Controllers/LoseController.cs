using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Behaviours;
using CodeBase.Collection;
using CodeBase.Factories;
using CodeBase.Game.Counters;
using CodeBase.ObjectType;
using CodeBase.Vibration;
using UnityEngine;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Game.Controllers
{
    public class LoseController
    {
        private const float AppearanceUIDelay = 1f;
        
        private readonly GameFactory _gameFactory;
        private readonly UIFactory _uiFactory;
        private readonly KnivesCollection _knivesCollection;
        private readonly List<Knife> _knivesList;
        private readonly StagesCounter _stagesCounter;
        private readonly KnivesCounter _knivesCounter;
        private readonly ScoreCounter _scoreCounter;

        public LoseController(GameFactory gameFactory, UIFactory uiFactory, KnivesCollection knivesCollection, StagesCounter stagesCounter, KnivesCounter knivesCounter, ScoreCounter scoreCounter)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _knivesCollection = knivesCollection;
            _stagesCounter = stagesCounter;
            _knivesCounter = knivesCounter;
            _knivesList = knivesCollection.KnivesList;
            _scoreCounter = scoreCounter;
        }
        
        public void OnLose(GameObject playerKnife, Knife collision)
        {
            StopBeamMotion();
            playerKnife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            playerKnife.gameObject.GetComponent<KnifeInput>().enabled = false;
            Motion motion = playerKnife.GetComponent<Motion>();
            motion.StopMove();
            motion.MoveBack();
            motion.StartRandomRotation();
            CreateLoseScreen();
            DestroyGameObjects();
            ResetCounters();
            MainVibration.Vibrate();
        }

        private void StopBeamMotion() => 
            _gameFactory.Enemy.GetComponent<EnemyMotion>().StopRotation();

        private async void CreateLoseScreen()
        {
            await Task.Delay(TimeSpan.FromSeconds(AppearanceUIDelay));
            _uiFactory.CreateLoseScreen();
        }

        private async void DestroyGameObjects()
        {
            await Task.Delay(TimeSpan.FromSeconds(AppearanceUIDelay));
            DestroyEnemy();
            DestroyApple();
            DestroyKnives();
            DestroyContainer();
        }

        private async void ResetCounters()
        {
            await Task.Delay(TimeSpan.FromSeconds(AppearanceUIDelay));
            _stagesCounter.ResetStages();
            _knivesCounter.UpdateCounter();
            _scoreCounter.ResetScore();
        }

        private void DestroyContainer() => 
            _gameFactory.DestroyContainer();

        private void DestroyEnemy() => 
            _gameFactory.DestroyEnemy();

        private void DestroyApple() => 
            _gameFactory.DestroyApple(0f);

        private void DestroyKnives()
        {
            foreach (Knife knife in _knivesList) 
                _gameFactory.DestroyKnife(knife, 0f);
            
            _knivesCollection.Clear();
        }
    }
}