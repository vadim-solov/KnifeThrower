using System;
using System.Collections.Generic;
using CodeBase.Behaviours;
using UnityEngine;
using CodeBase.Game;
using CodeBase.ObjectType;
using Motion = CodeBase.Behaviours.Motion;
using Random = UnityEngine.Random;

namespace CodeBase.Factories
{
    [CreateAssetMenu]
    public class GameFactory : ScriptableObject, IGameFactory
    {
        [Header("Stages config")]
        [SerializeField]
        private StageConfig[] _stageConfigs = new StageConfig[5];

        [Header("Config on all stages")]
        [SerializeField, Range(0f,100f)]
        private int _appleChance = 25;
        
        [Header("Attached object")]
        [SerializeField]
        private GameObject _attachedApple;
        [SerializeField]
        private GameObject _attachedKnife;
        
        [Header("Player object")]
        [SerializeField]
        private GameObject _playerKnife;
        
        private readonly float _applePosition = 60f;
        private readonly List<float> _knivesPositions = new List<float>() {0f, 120f, 240f};
        
        private GameObject _container;
        private GameObject _beam;
        private GameObject _apple;
        private LoseController _loseController;
        private VictoryController _victoryController;

        public StageConfig[] StageConfig => _stageConfigs;
        public GameObject Beam => _beam;
        public GameObject Apple => _apple;

        public event Action<Knife> KnifeCreated;
        
        public void Initialize(LoseController loseController, VictoryController victoryController)
        {
            _loseController = loseController;
            _victoryController = victoryController;
        }

        public void CreateContainer() => 
            _container = new GameObject {name = "Container"};

        public void CreateBeam()
        {
            _beam = Instantiate(_stageConfigs[0].Beam, _container.transform);
            var rb = AddRigidbody(_beam);
            AddBeam();
            var motion = _beam.AddComponent<Motion>();
            motion.Initialize(rb);
            motion.IsKinematic();
            motion.FreezePosition();
            motion.StartRotation(_stageConfigs[0].RotateSpeed);
        }

        public void CreateApple()
        {
            if(!CheckAppleChance())
                return;
            
            _apple = Instantiate(_attachedApple, _container.transform);
            Rigidbody rb = AddRigidbody(_apple);
            AddApple(_apple);
            Motion motion = AddMotion(_apple);
            motion.Initialize(rb);
            motion.SetDepth(_beam.transform);
            motion.SetPosition(_beam.transform, _applePosition);
            Attach(_apple);
        }

        public void CreateAttachedKnives()
        {
            int knivesCount = Random.Range(1, _knivesPositions.Count + 1);
            
            for (int i = 0; i < knivesCount; i++)
            {
                GameObject knife = Instantiate(_attachedKnife, _container.transform);
                Rigidbody rb = AddRigidbody(knife);
                AddKnife(knife);
                Motion motion = AddMotion(knife);
                motion.Initialize(rb);
                motion.Rotate();
                motion.SetDepth(_beam.transform);
                motion.SetPosition(_beam.transform, _knivesPositions[i]);
                Attach(knife);
                Knife knifeComponent = knife.GetComponent<Knife>();
                KnifeCreated?.Invoke(knifeComponent);
            }
        }

        public void CreatePlayerKnife()
        {
            GameObject knife = Instantiate(_playerKnife, _container.transform);
            Rigidbody rb = AddRigidbody(knife);
            AddKnife(knife);
            Motion motion = AddMotion(knife);
            motion.Initialize(rb);
            motion.SetPlayerKnife();
            AddKnifeInput(knife, motion);
            AddCollisionChecker(knife); 
            Knife knifeComponent = knife.GetComponent<Knife>();
            KnifeCreated?.Invoke(knifeComponent);
        }

        public void DestroyBeam() => 
            Destroy(_beam.gameObject);

        public void DestroyApple(float destructionTime) => 
            Destroy(_apple.gameObject, destructionTime);

        public void DestroyKnife(Knife knife, float destructionTime) => 
            Destroy(knife.gameObject, destructionTime);

        private bool CheckAppleChance()
        {
            int random = Random.Range(1, 101);
            Debug.Log(random);
            return random <= _appleChance;
        }

        private void Attach(GameObject entity)
        {
            FixedJoint joint = entity.AddComponent<FixedJoint>();
            joint.connectedBody = _beam.gameObject.GetComponent<Rigidbody>();
        }

        private void AddBeam() => 
            _beam.AddComponent<Beam>();

        private Motion AddMotion(GameObject entity) => 
            entity.AddComponent<Motion>();

        private Rigidbody AddRigidbody(GameObject entity)
        {
            entity.AddComponent<Rigidbody>();
            Rigidbody rb = entity.GetComponent<Rigidbody>();
            return rb;
        }

        private void AddApple(GameObject apple) => 
            apple.AddComponent<Apple>();

        private void AddKnife(GameObject knife) => 
            knife.AddComponent<Knife>();

        private void AddKnifeInput(GameObject knife, Motion motion) => 
            knife.AddComponent<KnifeInput>().Initialize(motion, 10f);

        private void AddCollisionChecker(GameObject knife) => 
            knife.AddComponent<CollisionChecker>().Initialize(_loseController, _victoryController);
    }
}