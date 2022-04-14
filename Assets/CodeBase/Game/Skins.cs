using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Factories;
using UnityEngine;

namespace CodeBase.Game
{
    [CreateAssetMenu]
    public class Skins : ScriptableObject
    {
        [SerializeField]
        private GameObject _defaultKnifePrefab;
        [SerializeField]
        private List<SkinConfig> _skinConfigs = new List<SkinConfig>();

        private UIFactory _uiFactory;

        public GameObject DefaultKnifePrefab => _defaultKnifePrefab;
        public List<SkinConfig> SkinConfigs => _skinConfigs;

        public void Initialize(UIFactory uiFactory) => 
            _uiFactory = uiFactory;

        public void CheckNewSkins(int stage)
        {
            for (int i = 0; i < _skinConfigs.Count; i++)
            {
                if (stage == (_skinConfigs[i].AvailableAtStage - 1) && _skinConfigs[i].Open == false)
                {
                    _skinConfigs[i].UnblockSkin();

                    var sprite = _skinConfigs[i].KnifePrefab.GetComponentInChildren<SpriteRenderer>().sprite;
                    _uiFactory.CreatNewSkinWindow(sprite);     
                    _uiFactory.DestroyNewSkinWindow(2f);
                }
            }
        }
    }
}