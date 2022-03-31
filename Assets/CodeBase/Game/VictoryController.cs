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
        private readonly StagesCounter _stagesCounter;

        public event Action Victory;

        public VictoryController(GameFactory gameFactory, KnivesCounter knivesCounter, KnivesCollection knivesCollection, StagesCounter stagesCounter)
        {
            _gameFactory = gameFactory;
            _knivesCounter = knivesCounter;
            _knivesList = knivesCollection.KnivesList;
            _knivesCollection = knivesCollection;
            _stagesCounter = stagesCounter;
            _knivesCounter.Victory += OnVictory;
        }
        
        public void Cleanup() => 
            _knivesCounter.Victory -= OnVictory;


        private void OnVictory()
        {
            DestroyBeam();
            TryDestroyApple();
            DestroyKnives();
            _knivesCollection.Clear();
            CreateNewObjects();
        }

        private async void CreateNewObjects()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            _stagesCounter.IncreaseStage();
            _knivesCounter.UpdateCounter();
            
            _gameFactory.CreateBeam();
            _gameFactory.CreateApple();
            _gameFactory.CreateAttachedKnives();
            _gameFactory.CreatePlayerKnife();

            Victory?.Invoke();
        }

        private void DestroyBeam() => 
            _gameFactory.DestroyBeam();

        private void TryDestroyApple()
        {
            if(_gameFactory.Apple == null)
                return;
            
            var apple = _gameFactory.Apple.GetComponent<Motion>();
            apple.Drop();
            _gameFactory.DestroyApple(2f);
        }

        private void DestroyKnives()
        {
            foreach (Knife knife in _knivesList)
            {
                var motion = knife.GetComponent<Motion>();
                motion.Drop();
                _gameFactory.DestroyKnife(knife, 2f);
            }
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
            playerKnife.transform.position += new Vector3(0f, 0.2f, 0f); // FIX
            FixedJoint joint = playerKnife.AddComponent<FixedJoint>();
            joint.connectedBody = beam.gameObject.GetComponent<Rigidbody>();
            playerKnife.gameObject.GetComponent<CollisionChecker>().SwitchOff();
            playerKnife.gameObject.GetComponent<KnifeInput>().enabled = false;
            _knivesCounter.Decrease();
            TryCreatePlayerKnife();
        }
    }
}