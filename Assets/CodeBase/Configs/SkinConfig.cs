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
        private int _availableAtStage;
        [SerializeField]
        private bool _open;

        public GameObject KnifePrefab => _knifePrefab;
        public int AvailableAtStage => _availableAtStage;
        public bool Open => _open;

        public void UnblockSkin() => 
            _open = true;
    }
}