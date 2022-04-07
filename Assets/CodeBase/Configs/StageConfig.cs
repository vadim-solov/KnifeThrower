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
        private GameObject _beam;
        [SerializeField]
        private float _rotateSpeed = 50f;
        [SerializeField]
        private float _rotatedTime = 5f;
        [SerializeField]
        private bool _boss;
        [SerializeField]
        private string _name;
        [SerializeField]
        private ParticleSystem _particleSystem;

        public GameObject Beam => _beam;
        public float RotateSpeed => _rotateSpeed;
        public int NumberOfKnives => _numberOfKnives;
        public bool Boss => _boss;
        public string Name => _name;
        public ParticleSystem ParticleSystem => _particleSystem;
    }
}