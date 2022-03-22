using System;
using UnityEngine;

namespace CodeBase.Factories
{
    [Serializable]
    public class StageConfig
    {
        [SerializeField]
        private ObjectType.Beam _beam;
        [SerializeField]
        private int _knifeCount = 5;

        public ObjectType.Beam Beam => _beam;
    }
}