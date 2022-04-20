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

        private readonly KnivesCollection _knivesCollection;
        private readonly StagesCounter _stagesCounter;
        private readonly KnivesCounter _knivesCounter;
        private readonly ScoreCounter _scoreCounter;
        
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public LoseController(IGameFactory gameFactory, IUIFactory uiFactory, KnivesCollection knivesCollection, StagesCounter stagesCounter, KnivesCounter knivesCounter, ScoreCounter scoreCounter)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _knivesCollection = knivesCollection;
            _stagesCounter = stagesCounter;
            _knivesCounter = knivesCounter;
            _scoreCounter = scoreCounter;
        }
        
        public void OnLose(GameObject playerKnife, Knife collision)
        {
            StopEnemyMotion();
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

        private void StopEnemyMotion() => 
            _gameFactory.Enemy.GetComponent<EnemyMotion>().StopRotation();

        private async void CreateLoseScreen()
        {
            await Task.Delay(TimeSpan.FromSeconds(AppearanceUIDelay));
            _uiFactory.CreateLoseScreen();
        }

        private async void DestroyGameObjects()
        {
            await Task.Delay(TimeSpan.FromSeconds(AppearanceUIDelay));
            _uiFactory.HideStage();
            _uiFactory.HideScore();
            _gameFactory.DestroyEnemy();
            _gameFactory.TryDestroyApple(0f);
            DestroyKnives();
            _gameFactory.DestroyContainer();
        }

        private void DestroyKnives()
        {
            foreach (Knife knife in _knivesCollection.KnivesList) 
                _gameFactory.DestroyKnife(knife, 0f);
            
            _knivesCollection.Clear();
        }

        private async void ResetCounters()
        {
            await Task.Delay(TimeSpan.FromSeconds(AppearanceUIDelay));
            _stagesCounter.ResetStages();
            _knivesCounter.UpdateCounter();
            _scoreCounter.ResetScore();
        }
    }
}