using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Collection;
using CodeBase.Factories;
using CodeBase.Game.Counters;
using CodeBase.ObjectType;
using CodeBase.Vibration;
using UnityEngine;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Game.Controllers
{
    public class VictoryController
    {
        private const float DestructionTime = 2f;
        private const string AnimationName = "SpawnKnife";

        private readonly GameFactory _gameFactory;
        private readonly KnivesCounter _knivesCounter;
        private readonly List<Knife> _knivesList;
        private readonly KnivesCollection _knivesCollection;
        private readonly StagesCounter _stagesCounter;
        private readonly Skins _skins;
        private readonly UIFactory _uiFactory;
        
        public bool IsVictory { get; private set; }

        public VictoryController(GameFactory gameFactory, KnivesCounter knivesCounter, KnivesCollection knivesCollection, StagesCounter stagesCounter, Skins skins, UIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _knivesCounter = knivesCounter;
            _knivesList = knivesCollection.KnivesList;
            _knivesCollection = knivesCollection;
            _stagesCounter = stagesCounter;
            _skins = skins;
            _uiFactory = uiFactory;
            _knivesCounter.Victory += OnVictory;
        }
        
        public void Cleanup() => 
            _knivesCounter.Victory -= OnVictory;

        private void OnVictory()
        {
            IsVictory = true;
            Vibrate();
            DestroyEnemy();
            CrateParticles();
            DestroyKnives();
            CreateKnivesParticles();
            TryDestroyApple();
            ClearCollection();
            CheckNewSkins();
            
            if (_stagesCounter.CheckMaxCompletedStage() == true)
            {
                CreateMaxStageScreen();
                return;
            }
            
            IncreaseStage();
            UpdateCounter();
            CreateNewObjects();
        }

        private void UpdateCounter() => 
            _knivesCounter.UpdateCounter();

        private void IncreaseStage() => 
            _stagesCounter.IncreaseStage();

        private void CheckNewSkins() => 
            _skins.CheckNewSkins();

        private void ClearCollection() => 
            _knivesCollection.Clear();

        private void CrateParticles() => 
            _gameFactory.CreateParticlesEnemyExplosion();

        private void Vibrate() => 
            MainVibration.Vibrate();

        private void CreateKnivesParticles()
        {
            foreach (var knife in _knivesList)
            {
                Sprite sprite = knife.GetComponentInChildren<SpriteRenderer>().sprite;
                _gameFactory.CreateKnivesParticles(sprite);
            }
        }

        private async void CreateMaxStageScreen()
        {
            await Task.Delay(TimeSpan.FromSeconds(DestructionTime));
            _uiFactory.CreateMaxStageScreen();
        }

        private async void CreateNewObjects()
        {
            await Task.Delay(TimeSpan.FromSeconds(DestructionTime));
            _gameFactory.CreateEnemy();
            _gameFactory.CreateApple();
            _gameFactory.TryCreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
            _gameFactory.PlayerKnife.GetComponent<Animator>().SetBool(AnimationName, true);
            IsVictory = false;
        }

        private void DestroyEnemy() => 
            _gameFactory.DestroyEnemy();

        private void TryDestroyApple()
        {
            if(_gameFactory.Apple == null)
                return;
            
            Motion apple = _gameFactory.Apple.GetComponent<Motion>();
            apple.TurnOnGravity();
            apple.StartRandomRotation();
            _gameFactory.DestroyApple(DestructionTime);
        }

        private void DestroyKnives()
        {
            foreach (Knife knife in _knivesList) 
                _gameFactory.DestroyKnife(knife, 0f);
        }
    }
}