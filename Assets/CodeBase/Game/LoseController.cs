using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Behaviours;
using CodeBase.Collection;
using CodeBase.Factories;
using CodeBase.ObjectType;
using UnityEngine;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Game
{
    public class LoseController
    {
        private const float AppearanceUIDelay = 1f;
        
        private readonly GameFactory _gameFactory;
        private readonly UIFactory _uiFactory;
        private readonly KnivesCollection _knivesCollection;
        private readonly List<Knife> _knivesList;

        public LoseController(GameFactory gameFactory, UIFactory uiFactory, KnivesCollection knivesCollection)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _knivesCollection = knivesCollection;
            _knivesList = knivesCollection.KnivesList;
        }
        
        public void OnLose(GameObject playerKnife, Knife collision)
        {
            Debug.Log("Lose");
            StopBeamMotion();

            playerKnife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            playerKnife.gameObject.GetComponent<KnifeInput>().enabled = false;

            var motion = playerKnife.GetComponent<Motion>();
            motion.StopMove();
            motion.MoveBack();
            motion.StartRandomRotation();
            
            CreateLoseScreen();
            
            DestroyBeam();
            DestroyApple();
            DestroyKnives();
        }

        private async void CreateLoseScreen()
        {
            await Task.Delay(TimeSpan.FromSeconds(AppearanceUIDelay));
            _uiFactory.CreateLoseScreen();
        }

        private void StopBeamMotion() => 
            _gameFactory.Beam.GetComponent<Motion>().StopRotation();

        private async void DestroyBeam()
        {
            await Task.Delay(TimeSpan.FromSeconds(AppearanceUIDelay));
            _gameFactory.DestroyBeam();
        }

        private async void DestroyApple()
        {
            await Task.Delay(TimeSpan.FromSeconds(AppearanceUIDelay));
            _gameFactory.DestroyApple(0f);
        }

        private async void DestroyKnives()
        {
            await Task.Delay(TimeSpan.FromSeconds(AppearanceUIDelay));
            
            foreach (Knife knife in _knivesList) 
                _gameFactory.DestroyKnife(knife, 0f);
            
            _knivesCollection.Clear();
        }
    }
}