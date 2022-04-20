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
        private const string SpawnKnifeAnimation = "SpawnKnife";
        private const string IsGrowthAnimation = "IsGrowth";

        private readonly KnivesCounter _knivesCounter;
        private readonly KnivesCollection _knivesCollection;
        private readonly StagesCounter _stagesCounter;
        private readonly Skins _skins;
        
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        
        public bool IsVictory { get; private set; }

        public event Action Victory;

        public VictoryController(IGameFactory gameFactory, KnivesCounter knivesCounter, KnivesCollection knivesCollection, StagesCounter stagesCounter, Skins skins, IUIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _knivesCounter = knivesCounter;
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
            MainVibration.Vibrate();
            _gameFactory.DestroyEnemy();
            _gameFactory.CreateParticlesEnemyExplosion();
            DestroyKnives();
            CreateKnivesParticles();
            TryDestroyApple();
            _knivesCollection.Clear();
            _skins.CheckNewSkins();
            
            if (_stagesCounter.CheckMaxCompletedStage() == true)
            {
                CreateMaxStageScreen();
                return;
            }
            
            _stagesCounter.IncreaseStage();
            _knivesCounter.UpdateCounter();
            CreateNewObjects();
            Victory?.Invoke();
        }
        
        private void CreateKnivesParticles()
        {
            foreach (var knife in _knivesCollection.KnivesList)
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
            EnabledEnemyAnimation();
            EnableKnivesAnimation();
            EnablePlayerKnifeAnimations();
            IsVictory = false;
        }

        private void EnabledEnemyAnimation() => 
            _gameFactory.Enemy.GetComponent<Animator>().SetBool(IsGrowthAnimation, true);

        private void EnableKnivesAnimation()
        {
            foreach (var knife in _knivesCollection.KnivesList)
                knife.GetComponent<Animator>().SetBool(IsGrowthAnimation, true);
        }

        private void EnablePlayerKnifeAnimations() => 
            _gameFactory.PlayerKnife.GetComponent<Animator>().SetBool(SpawnKnifeAnimation, true);

        private void TryDestroyApple()
        {
            if(_gameFactory.Apple == null)
                return;
            
            Motion apple = _gameFactory.Apple.GetComponent<Motion>();
            apple.TurnOnGravity();
            apple.StartRandomRotation();
            _gameFactory.TryDestroyApple(DestructionTime);
        }

        private void DestroyKnives()
        {
            foreach (Knife knife in _knivesCollection.KnivesList) 
                _gameFactory.DestroyKnife(knife, 0f);
        }
    }
}