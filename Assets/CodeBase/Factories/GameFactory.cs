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
        
        private int _maxKnife = 3;
        private GameObject _container;
        private GameObject _beam;
        private GameObject _apple;
        private HitController _hitController;

        public StageConfig[] StageConfig => _stageConfigs;

        public event Action<Knife> KnifeCreated;
        
        public void Initialize(HitController hitController) => 
            _hitController = hitController;

        public void CreateContainer()
        {
            _container = new GameObject {name = "Container"};
        }

        public void CreateBeam()
        {
            _beam = Instantiate(_stageConfigs[0].Beam, _container.transform);
            AddRigidbodyComponent(_beam, true);
            _beam.AddComponent<Beam>();
            _beam.AddComponent<BeamMotion>().StartRotation(_stageConfigs[0].RotateSpeed);
        }

        public void CreateApple()
        {
            if(!CheckAppleChance())
                return;
            
            _apple = Instantiate(_attachedApple, _container.transform);
            AddRigidbodyComponent(_apple, false);
            SetPosition(_apple.transform, _beam.transform);
            SetAngle(_apple.transform, _beam.transform, _applePosition);
            Attach(_apple);
            AddAppleComponent(_apple);
        }

        public void CreateKnives()
        {
            int knivesCount = Random.Range(1, _maxKnife + 1);
            //int knivesCount = 0;
            
            for (int i = 0; i < knivesCount; i++)
            {
                GameObject knife = Instantiate(_attachedKnife, _container.transform);
                var rb = AddRigidbodyComponent(knife, false);
                SetPosition(knife.transform, _beam.transform);
                SetAngle(knife.transform, _beam.transform, _knivesPositions[0]);
                Attach(knife);
                AddKnifeComponent(knife);
                knife.AddComponent<Motion>().Initialize(rb, 10f);
                RemoveInListPositions();

                var knifeComponent = knife.GetComponent<Knife>();
                KnifeCreated?.Invoke(knifeComponent);
            }
        }

        public void CreatePlayerKnife()
        {
            GameObject knife = Instantiate(_playerKnife, _container.transform);
            var rb = AddRigidbodyComponent(knife, false);
            AddKnifeComponent(knife);
            knife.AddComponent<Motion>().Initialize(rb, 10f);
            var motion = knife.GetComponent<Motion>();
            AddKnifeInputComponent(knife, motion);
            AddCollisionCheckerComponent(knife);
            SetPosition(knife.transform);
            
            var knifeComponent = knife.GetComponent<Knife>();
            KnifeCreated?.Invoke(knifeComponent);
        }

        private bool CheckAppleChance()
        {
            int random = Random.Range(1, 101);
            Debug.Log(random);
            return random <= _appleChance;
        }

        private void RemoveInListPositions()
        {
            _knivesPositions.RemoveAt(0);
            Debug.Log(_knivesPositions.Count);
        }

        public void DestroyBeam() => 
            Destroy(_beam.gameObject);
        
        public void DestroyApple() => 
            Destroy(_apple.gameObject, 2f);

        public void DestroyKnife(Knife knife) => 
            Destroy(knife.gameObject, 2f);

        private Rigidbody AddRigidbodyComponent(GameObject entity, bool freezePosition)
        {
            entity.AddComponent<Rigidbody>();
            var rb = entity.GetComponent<Rigidbody>();
            rb.mass = 0f;
            rb.useGravity = false;

            if (freezePosition)
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

            return rb;
        }

        private void Attach(GameObject entity)
        {
            FixedJoint joint = entity.AddComponent<FixedJoint>();
            joint.connectedBody = _beam.gameObject.GetComponent<Rigidbody>();
        }

        private void AddAppleComponent(GameObject apple) => 
            apple.AddComponent<Apple>();

        private void AddKnifeComponent(GameObject knife) => 
            knife.AddComponent<Knife>();

        private void AddKnifeInputComponent(GameObject knife, Motion motion) => 
            knife.AddComponent<KnifeInput>().Initialize(motion);

        private void AddCollisionCheckerComponent(GameObject knife) => 
            knife.AddComponent<CollisionChecker>().Initialize(_hitController);

        private void SetPosition(Transform entity) => 
            entity.position = new Vector3(0f, -4f, 0f);

        private void SetPosition(Transform entity, Transform beam) => 
            entity.position = beam.transform.position + new Vector3(0.7f, 0f, 0f);

        private void SetAngle(Transform entity, Transform beam, float angle) => 
            entity.transform.RotateAround(beam.transform.position, Vector3.forward, angle);
    }
}