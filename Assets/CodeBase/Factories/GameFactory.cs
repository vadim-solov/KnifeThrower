using System;
using System.Collections.Generic;
using CodeBase.Behaviours;
using CodeBase.Configs;
using UnityEngine;
using CodeBase.Game;
using CodeBase.Game.Controllers;
using CodeBase.Game.Counters;
using CodeBase.Game.Hit;
using CodeBase.ObjectType;
using UnityEditor.Animations;
using Motion = CodeBase.Behaviours.Motion;
using Random = UnityEngine.Random;

namespace CodeBase.Factories
{
    [CreateAssetMenu]
    public class GameFactory : ScriptableObject, IGameFactory
    {
        private const string ContainerName = "Container";
        private const float AppleAttachmentDepth = 0.67f;
        private const float KnifeAttachmentDepth = 0.3f;
        private const float MarginLogImpactParticles = 0.73f;

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
        private GameObject _defaultKnife;
        [SerializeField] 
        private GameObject _skinKnife;
        [SerializeField]
        private AnimatorController _animatorController;

        [Header("Particles")]
        [SerializeField]
        private ParticleSystem _particlesOnImpact;
        [SerializeField]
        private ParticleSystem _appleParticles;
        [SerializeField]
        private ParticleSystem _knivesParticles;

        private GameObject _container;
        private GameObject _log;
        private GameObject _apple;
        private LoseController _loseController;
        private StagesCounter _stagesCounter;
        private AppleHit _appleHit;
        private LogHit _logHit;
        private GameObject _playerKnife;

        private readonly float _applePosition = 60f;
        private readonly List<float> _knivesPositions = new List<float>() {0f, 120f, 240f};

        public StageConfig[] StageConfig => _stageConfigs;
        public GameObject Log => _log;
        public GameObject Apple => _apple;
        public GameObject PlayerKnife => _playerKnife;

        public event Action<Knife> KnifeCreated;
        public event Action AttachedKnivesCreated;
        
        public void Initialize(LoseController loseController, StagesCounter stagesCounter, AppleHit appleHit, LogHit logHit)
        {
            _loseController = loseController;
            _stagesCounter = stagesCounter;
            _appleHit = appleHit;
            _logHit = logHit;
        }

        public void CreateContainer() => 
            _container = new GameObject {name = ContainerName};

        public void CreateLog()
        {
            _log = Instantiate(_stageConfigs[_stagesCounter.Stage].LogPrefab, _container.transform);
            AddLog();
            LogMotion motion = _log.AddComponent<LogMotion>();
            motion.LogInitialize(_stageConfigs[_stagesCounter.Stage].RotateSpeed, _stageConfigs[_stagesCounter.Stage].RotationTime, _stageConfigs[_stagesCounter.Stage].RotationStopTime);
            motion.SetPosition();
            motion.StartRotationTimer();
            motion.StartRotation();
            AddLogRigidbody2D(_log);
        }

        public void CreateApple()
        {
            if(!CheckAppleChance())
                return;
            
            _apple = Instantiate(_attachedApple, _container.transform);
            AddApple(_apple);
            Motion motion = AddMotion(_apple);
            motion.InitializeLog(_log);
            motion.SetPosition(_log.transform, _applePosition);
            var rb = AddRigidbody2D(_apple);
            AddAttach(_apple, AppleAttachmentDepth);
            motion.InitializeRigidbody2D(rb);
        }

        public void CreateAttachedKnives()
        {
            int knivesCount = Random.Range(1, _knivesPositions.Count + 1);
            
            for (int i = 0; i < knivesCount; i++)
            {
                GameObject knife = Instantiate(_attachedKnife, _container.transform);
                AddKnife(knife);
                Motion motion = AddMotion(knife);
                motion.InitializeLog(_log);
                motion.SetPosition(_log.transform, _knivesPositions[i]);
                var rb = AddRigidbody2D(knife);
                AddAttach(knife, KnifeAttachmentDepth);
                motion.InitializeRigidbody2D(rb);
                Knife knifeComponent = knife.GetComponent<Knife>();
                KnifeCreated?.Invoke(knifeComponent);
            }
            
            AttachedKnivesCreated?.Invoke();
        }

        public void CreatePlayerKnife()
        {
            if (_skinKnife == null)
                _playerKnife = Instantiate(_defaultKnife, _container.transform);
            
            else
                _playerKnife = Instantiate(_skinKnife, _container.transform);

            AddAnimator(_playerKnife);
            Rigidbody2D rb = AddRigidbody2D(_playerKnife);
            AddKnife(_playerKnife);
            Motion motion = AddMotion(_playerKnife);
            motion.InitializeLog(_log);
            motion.SetPositionPlayerKnife();
            motion.InitializeRigidbody2D(rb);
            AddKnifeInput(_playerKnife, motion);
            AddCollisionChecker(_playerKnife);
            Knife knifeComponent = _playerKnife.GetComponent<Knife>();
            KnifeCreated?.Invoke(knifeComponent);
        }

        private void AddAnimator(GameObject entity)
        {
            Animator animator = entity.AddComponent<Animator>();
            animator.runtimeAnimatorController = _animatorController;
        }

        public void DestroyContainer() => 
            Destroy(_container.gameObject);

        public void DestroyBeam() => 
            Destroy(_log.gameObject);

        public void DestroyApple(float destructionTime) => 
            Destroy(_apple.gameObject, destructionTime);

        public void DestroyKnife(Knife knife, float destructionTime) => 
            Destroy(knife.gameObject, destructionTime);

        public void CreateParticlesOnExplosionLog() => 
            Instantiate(_stageConfigs[_stagesCounter.Stage].LogExplosionParticles, _container.transform);

        public void CreateParticlesOnImpact(Vector3 position) => 
            Instantiate(_particlesOnImpact, position + new Vector3(0f, MarginLogImpactParticles, 0f), _particlesOnImpact.transform.rotation, _container.transform);

        public void CreateAppleParticles(Vector3 position) => 
            Instantiate(_appleParticles, position, _appleParticles.transform.rotation, _container.transform);
        
        public void CreateKnivesParticles(Sprite sprite)
        {
            var particles = Instantiate(_knivesParticles, _container.transform);
            particles.textureSheetAnimation.AddSprite(sprite);
        }

        private bool CheckAppleChance()
        {
            int random = Random.Range(1, 101);
            return random <= _appleChance;
        }

        private void AddLog() => 
            _log.AddComponent<Log>();

        private Motion AddMotion(GameObject entity) => 
            entity.AddComponent<Motion>();

        private void AddApple(GameObject apple) => 
            apple.AddComponent<Apple>();

        private void AddKnife(GameObject knife) => 
            knife.AddComponent<Knife>();

        private void AddKnifeInput(GameObject knife, Motion motion) => 
            knife.AddComponent<KnifeInput>().Initialize(motion);

        private void AddCollisionChecker(GameObject knife) => 
            knife.AddComponent<CollisionChecker>().Initialize(_loseController, _appleHit, _logHit);

        public void ChangeKnifeSkin(GameObject knife)
        {
            _skinKnife = knife;
        }

        private Rigidbody2D AddLogRigidbody2D(GameObject entity)
        {
            var rb = entity.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            return rb;
        }      
        
        private Rigidbody2D AddRigidbody2D(GameObject entity)
        {
            var rb = entity.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.angularDrag = 0f;
            return rb;
        }

        public void AddAttach(GameObject entity, float attachmentDepth)
        {
            var rb = _log.GetComponent<Rigidbody2D>();
            var join = entity.AddComponent<FixedJoint2D>();
            join.connectedBody = rb;
            join.autoConfigureConnectedAnchor = false;
            join.anchor = new Vector2(0f, attachmentDepth);
        }
    }
}