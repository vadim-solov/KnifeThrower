using System;
using UnityEngine;

namespace CodeBase.Factories
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

        public GameObject Beam => _beam;
        public float RotateSpeed => _rotateSpeed;
        public int NumberOfKnives => _numberOfKnives;
    }
}