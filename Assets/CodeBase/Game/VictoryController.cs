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
    public class VictoryController
    {
        private readonly GameFactory _gameFactory;
        private readonly KnivesCounter _knivesCounter;
        private readonly List<Knife> _knivesList;
        private readonly KnivesCollection _knivesCollection;

        public VictoryController(GameFactory gameFactory, KnivesCounter knivesCounter, KnivesCollection knivesCollection)
        {
            _gameFactory = gameFactory;
            _knivesCounter = knivesCounter;
            _knivesList = knivesCollection.KnivesList;
            _knivesCollection = knivesCollection;
            _knivesCounter.Victory += OnVictory;
        }
        
        public void Cleanup()
        {
            _knivesCounter.Victory -= OnVictory;
            Debug.Log("Desub");
        }


        private void OnVictory()
        {
            Debug.Log("Victory!");
            
            DestroyBeam();
            DestroyApple();
            DestroyKnives();
            _knivesCollection.Clear();
            
            CreateNewObjects();
        }

        private async void CreateNewObjects()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();
            Debug.Log("!!!!!!!!!!!");
        }

        private void DestroyBeam() => 
            _gameFactory.DestroyBeam();

        private void DestroyApple() => 
            _gameFactory.DestroyApple();

        private void DestroyKnives()
        {
            foreach (Knife knife in _knivesList) 
                _gameFactory.DestroyKnife(knife);
        }
        
        private void TryCreatePlayerKnife()
        {
            if(_knivesCounter.CheckLastKnife())
                return;
            
            _gameFactory.CreatePlayerKnife();
        }

        public void OnHitInBeam(GameObject playerKnife, Beam beam)
        {
            var motion = playerKnife.GetComponent<Motion>();
            motion.StopMove();
            FixedJoint joint = playerKnife.AddComponent<FixedJoint>();
            joint.connectedBody = beam.gameObject.GetComponent<Rigidbody>();
            playerKnife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            playerKnife.gameObject.GetComponent<KnifeInput>().enabled = false;
            _knivesCounter.Decrease();
            TryCreatePlayerKnife();
        }
        
        public void OnHitInApple(GameObject knife, Apple component)
        {
            var motion = knife.GetComponent<Motion>();
            motion.StopMove();
            
            FixedJoint joint = knife.AddComponent<FixedJoint>();
            joint.connectedBody = component.gameObject.GetComponent<Rigidbody>();
            
            knife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            knife.gameObject.GetComponent<KnifeInput>().enabled = false;
        }
    }
}