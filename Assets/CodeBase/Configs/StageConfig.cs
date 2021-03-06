using System;
using UnityEngine;

namespace CodeBase.Configs
{
    [Serializable]
    public class StageConfig
    {
        [SerializeField]
        private bool _boss;
        [SerializeField]
        private string _name;
        [SerializeField]
        private GameObject _enemyPrefab;
        [SerializeField]
        private ParticleSystem _enemyExplosionParticles;
        [SerializeField, Range(1, 15)]
        private int _numberOfKnives = 5;
        [SerializeField, Range(-600, 600)]
        private float _rotateSpeed = 100f;
        [SerializeField, Range(0, 10)]
        private float _rotationTime = 3f;
        [SerializeField, Range(0, 10)]
        private float _rotationStopTime = 2f;
        [SerializeField, Range(30f, 1500f)]
        private float _startStopImpulse = 100f;

        public bool Boss => _boss;
        public string Name => _name;
        public GameObject EnemyPrefab => _enemyPrefab;
        public ParticleSystem EnemyExplosionParticles => _enemyExplosionParticles;
        public int NumberOfKnives => _numberOfKnives;
        public float RotateSpeed => _rotateSpeed;
        public float RotationTime => _rotationTime;
        public float RotationStopTime => _rotationStopTime;
        public float StartStopImpulse => _startStopImpulse;
    }
}