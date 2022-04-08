using System;
using UnityEngine;

namespace CodeBase.Configs
{
    [Serializable]
    public class StageConfig
    {
        [SerializeField]
        private int _numberOfKnives = 5;
        [SerializeField]
        private GameObject _logPrefab;
        [SerializeField]
        private float _rotateSpeed = 50f;
        [SerializeField]
        private float _rotationTime = 3f;
        [SerializeField]
        private float _rotationStopTime = 2f;
        [SerializeField]
        private bool _boss;
        [SerializeField]
        private string _name;
        [SerializeField]
        private ParticleSystem _logExplosionParticles;
        [SerializeField]
        private ParticleSystem _particlesOnImpact;

        public GameObject LogPrefab => _logPrefab;
        public float RotateSpeed => _rotateSpeed;
        public int NumberOfKnives => _numberOfKnives;
        public bool Boss => _boss;
        public string Name => _name;
        public ParticleSystem LogExplosionParticles => _logExplosionParticles;
        public ParticleSystem ParticlesOnImpact => _particlesOnImpact;
        public float RotationTime => _rotationTime;
        public float RotationStopTime => _rotationStopTime;
    }
}