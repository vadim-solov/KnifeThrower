using System.Collections.Generic;
using CodeBase.Beam;
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
        private ObjectType.Beam _beam;
        private HitController _hitController;
        
        public void Initialize(HitController hitController) => 
            _hitController = hitController;

        public void CreateContainer() => 
            _container = new GameObject();

        public void CreateBeam()
        {
            _beam = Instantiate(_stageConfigs[0].Beam, _container.transform);
            _beam.GetComponent<BeamMotion>().StartRotation();
        }

        public void CreateApple()
        {
            if(!CheckAppleChance())
                return;
            
            GameObject apple = Instantiate(_attachedApple, _container.transform);
            AddAppleComponent(apple);
            SetPosition(apple.transform, _beam.transform);
            SetAngle(apple.transform, _beam.transform, _applePosition);
            Attach(apple);
        }

        public void CreateKnives()
        {
            int knivesCount = Random.Range(1, _maxKnife + 1);

            for (int i = 0; i < knivesCount; i++)
            {
                GameObject knife = Instantiate(_attachedKnife, _container.transform);
                AddKnifeComponent(knife);
                SetPosition(knife.transform, _beam.transform);
                SetAngle(knife.transform, _beam.transform, _knivesPositions[0]);
                Attach(knife);
                RemoveInListPositions();
            }
        }

        public void CreatePlayerKnife()
        {
            GameObject knife = Instantiate(_playerKnife);
            AddKnifeComponent(knife);
            var rb = knife.GetComponent<Rigidbody>();
            knife.AddComponent<Motion>().Initialize(rb, 10f);
            var motion = knife.GetComponent<Motion>();
            AddKnifeInputComponent(knife, motion);
            AddCollisionCheckerComponent(knife);
            SetPosition(knife.transform);
        }

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

        private void RemoveInListPositions()
        {
            _knivesPositions.RemoveAt(0);
            Debug.Log(_knivesPositions.Count);
        }
    }
}