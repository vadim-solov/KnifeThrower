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
        
        public void Cleanup()
        {
            Debug.Log("Desub");
        }

        public void OnLose(GameObject playerKnife, Knife collision)
        {
            Debug.Log("Lose");
            StopMotion();
            _knivesCollection.Clear();
            CreateLoseScreen();
            
            var motion = playerKnife.GetComponent<Motion>();
            motion.StopMove();
            
            FixedJoint joint = playerKnife.AddComponent<FixedJoint>();
            joint.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
            
            playerKnife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            playerKnife.gameObject.GetComponent<KnifeInput>().enabled = false;
        }

        private async void CreateLoseScreen()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            _uiFactory.CreateLoseScreen();
            Debug.Log("!!!!!!!");
        }

        private void StopMotion() => 
            _gameFactory.Beam.GetComponent<Motion>().StopRotation();
    }
}