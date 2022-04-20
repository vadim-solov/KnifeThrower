using System;
using System.Collections.Generic;
using CodeBase.Behaviours;
using CodeBase.Configs;
using CodeBase.Game;
using UnityEngine;
using CodeBase.Game.Controllers;
using CodeBase.Game.Counters;
using CodeBase.Game.Hit;
using CodeBase.ObjectType;
using Motion = CodeBase.Behaviours.Motion;
using Random = UnityEngine.Random;

namespace CodeBase.Factories
{
    [CreateAssetMenu]
    public class GameFactory : ScriptableObject, IGameFactory
    {
        private const string ContainerName = "Container";
        private const float AppleAttachmentDepth = 0.4f;
        private const float KnifeAttachmentDepth = 0.22f;
        private const float MarginLogImpactParticles = 0.73f;
        private const float ApplePosition = 60f;

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
        
        [Header("Animations")]
        [SerializeField]
        private RuntimeAnimatorController  _spawnAnimation;
        [SerializeField]
        private RuntimeAnimatorController _growthAnimation;
                
        [Header("Particles")]
        [SerializeField]
        private ParticleSystem _particlesOnImpact;
        [SerializeField]
        private ParticleSystem _appleParticles;
        [SerializeField]
        private ParticleSystem _knivesParticles;

        private GameObject _container;
        private LoseController _loseController;
        private StagesCounter _stagesCounter;
        private AppleHit _appleHit;
        private EnemyHit _enemyHit;
        private Skins _skins;

        private readonly List<float> _knivesPositions = new List<float>() {0f, 120f, 240f};
        private readonly Vector3 _playerKnifeStartPosition = new Vector3(0f, -2.8f, 0f);
        private readonly Vector3 _enemyStartPosition = new Vector3(0f, 1f, 0f);

        public StageConfig[] StageConfig => _stageConfigs;
        public GameObject Enemy { get; private set; }
        public GameObject Apple { get; private set; }
        public GameObject PlayerKnife { get; private set; }

        public event Action EnemyCreated;
        public event Action<Knife> KnifeCreated;
        public event Action AttachedKnivesCreated;

        public void Initialize(LoseController loseController, StagesCounter stagesCounter, AppleHit appleHit, EnemyHit enemyHit, Skins skins)
        {
            _loseController = loseController;
            _stagesCounter = stagesCounter;
            _appleHit = appleHit;
            _enemyHit = enemyHit;
            _skins = skins;
        }

        public void CreateContainer() => 
            _container = new GameObject {name = ContainerName};

        public void CreateEnemy()
        {
            Enemy = Instantiate(_stageConfigs[_stagesCounter.CurrentStage].EnemyPrefab, _container.transform);
            AddAnimator(Enemy, _growthAnimation);
            AddEnemy();
            EnemyMotion motion = Enemy.AddComponent<EnemyMotion>();
            motion.EnemyInitialize(_enemyStartPosition, _stageConfigs[_stagesCounter.CurrentStage].RotateSpeed, _stageConfigs[_stagesCounter.CurrentStage].RotationTime,
                _stageConfigs[_stagesCounter.CurrentStage].RotationStopTime, _stageConfigs[_stagesCounter.CurrentStage].StartStopImpulse);
            motion.SetStartPosition();
            motion.StartRotation();
            AddLogRigidbody2D(Enemy);
            EnemyCreated?.Invoke();
        }

        public void CreateApple()
        {
            if(!CheckAppleChance())
                return;
            
            Apple = Instantiate(_attachedApple, _container.transform);
            AddApple(Apple);
            Motion motion = AddMotion(Apple);
            motion.SetStartPositionAroundEnemy(_enemyStartPosition, ApplePosition);
            Rigidbody2D rb = AddRigidbody2D(Apple);
            AddAttach(Apple, AppleAttachmentDepth);
            motion.Initialize(rb);
        }

        public void TryCreateAttachedKnives()
        {
            AttachedKnivesCreated?.Invoke();
            
            if(_stageConfigs[_stagesCounter.CurrentStage].Boss == true)
                return;

            int knivesCount = Random.Range(1, _knivesPositions.Count + 1);
            
            for (int i = 0; i < knivesCount; i++)
            {
                GameObject knife = Instantiate(_attachedKnife, _container.transform);
                AddAnimator(knife, _growthAnimation);
                AddKnife(knife);
                Motion motion = AddMotion(knife);
                motion.SetStartPositionAroundEnemy(_enemyStartPosition, _knivesPositions[i]);
                Rigidbody2D rb = AddRigidbody2D(knife);
                AddAttach(knife, KnifeAttachmentDepth);
                motion.Initialize(rb);
                Knife knifeComponent = knife.GetComponent<Knife>();
                KnifeCreated?.Invoke(knifeComponent);
            }
        }

        public void CreatePlayerKnife()
        {
            for (int i = 0; i < _skins.SkinConfigs.Count; i++)
            {
                if (_skins.CurrentSkin == i)
                    PlayerKnife = Instantiate(_skins.SkinConfigs[i].KnifePrefab, _container.transform);
            }
            
            AddAnimator(PlayerKnife, _spawnAnimation);
            Rigidbody2D rb = AddRigidbody2D(PlayerKnife);
            AddKnife(PlayerKnife);
            Motion motion = AddMotion(PlayerKnife);
            motion.SetStartPositionPlayerKnife(_playerKnifeStartPosition);
            motion.Initialize(rb);
            AddKnifeInput(PlayerKnife, motion);
            AddCollisionChecker(PlayerKnife);
            Knife knifeComponent = PlayerKnife.GetComponent<Knife>();
            KnifeCreated?.Invoke(knifeComponent);
        }

        public void DestroyContainer() => 
            Destroy(_container.gameObject);

        public void DestroyEnemy() => 
            Destroy(Enemy.gameObject);

        public void DestroyApple(float destructionTime) => 
            Destroy(Apple.gameObject, destructionTime);

        public void DestroyKnife(Knife knife, float destructionTime) => 
            Destroy(knife.gameObject, destructionTime);

        public void CreateParticlesEnemyExplosion() => 
            Instantiate(_stageConfigs[_stagesCounter.CurrentStage].EnemyExplosionParticles, _container.transform);

        public void CreateParticlesOnImpact(Vector3 position) => 
            Instantiate(_particlesOnImpact, position + new Vector3(0f, MarginLogImpactParticles, 0f), _particlesOnImpact.transform.rotation, _container.transform);

        public void CreateAppleParticles(Vector3 position) => 
            Instantiate(_appleParticles, position, _appleParticles.transform.rotation, _container.transform);

        public void CreateKnivesParticles(Sprite sprite)
        {
            var particles = Instantiate(_knivesParticles, _container.transform);
            particles.textureSheetAnimation.AddSprite(sprite);
        }

        public void AddAttach(GameObject entity, float attachmentDepth)
        {
            Rigidbody2D rb = Enemy.GetComponent<Rigidbody2D>();
            FixedJoint2D join = entity.AddComponent<FixedJoint2D>();
            join.connectedBody = rb;
            join.autoConfigureConnectedAnchor = false;
            join.anchor = new Vector2(0f, attachmentDepth);
        }

        private bool CheckAppleChance()
        {
            int random = Random.Range(1, 101);
            return random <= _appleChance;
        }

        private void AddAnimator(GameObject entity, RuntimeAnimatorController animationType)
        {
            Animator animator = entity.AddComponent<Animator>();
            animator.runtimeAnimatorController = animationType;
        }

        private void AddEnemy() => 
            Enemy.AddComponent<Enemy>();

        private Motion AddMotion(GameObject entity) => 
            entity.AddComponent<Motion>();

        private void AddApple(GameObject apple) => 
            apple.AddComponent<Apple>();

        private void AddKnife(GameObject knife) => 
            knife.AddComponent<Knife>();

        private void AddKnifeInput(GameObject knife, Motion motion) => 
            knife.AddComponent<KnifeInput>().Initialize(motion);

        private void AddCollisionChecker(GameObject knife) => 
            knife.AddComponent<CollisionChecker>().Initialize(_loseController, _appleHit, _enemyHit);

        private void AddLogRigidbody2D(GameObject entity)
        {
            var rb = entity.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }

        private Rigidbody2D AddRigidbody2D(GameObject entity)
        {
            var rb = entity.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.angularDrag = 0f;
            return rb;
        }
    }
}