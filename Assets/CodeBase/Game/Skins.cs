using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Configs;
using CodeBase.Factories;
using UnityEngine;

namespace CodeBase.Game
{
    [CreateAssetMenu]
    public class Skins : ScriptableObject
    {
        private const string StartDownUp = "StartUpDown";
        private const string StartUpDown = "StartDownUp";
        private const float WindowDestroyTime = 4f;

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
                    var window = _uiFactory.CreatNewSkinWindow(sprite);
                    StartUpDownAnimation(window);
                    StartDownUpAnimation(window);
                    DestroyNewSkinWindow();
                }
            }
        }

        private void StartUpDownAnimation(RectTransform window) => 
            window.GetComponent<Animator>().SetBool(StartDownUp, true);

        private async void StartDownUpAnimation(RectTransform window)
        {
            await Task.Delay(TimeSpan.FromSeconds(2f));
            window.GetComponent<Animator>().SetBool(StartUpDown, true);
        }

        private async void DestroyNewSkinWindow()
        {
            await Task.Delay(TimeSpan.FromSeconds(WindowDestroyTime));
            _uiFactory.DestroyNewSkinWindow();
        }
    }
}