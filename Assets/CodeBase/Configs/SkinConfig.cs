using System;
using UnityEngine;

namespace CodeBase.Configs
{
    [Serializable]
    public class SkinConfig
    {
        [SerializeField]
        private GameObject _knifePrefab;
        [SerializeField]
        private int _opensAfterStage;

        public GameObject KnifePrefab => _knifePrefab;
        public int OpensAfterStage => _opensAfterStage;
    }
}