using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Configs;
using CodeBase.Factories;
using CodeBase.Game.Counters;
using CodeBase.SaveLoadSystem;
using UnityEngine;

namespace CodeBase.Game
{
    [CreateAssetMenu]
    public class Skins : ScriptableObject
    {
        [SerializeField]
        private List<SkinConfig> _skinConfigs = new List<SkinConfig>();

        private const string StartDownUp = "StartUpDown";
        private const string StartUpDown = "StartDownUp";
        private const float WindowDestroyTime = 4f;
        
        private StagesCounter _stagesCounter;
        
        private IUIFactory _uiFactory;
        private ISaveLoadSystem _saveLoadSystem;

        public int CurrentSkin { get; private set; }
        public List<SkinConfig> SkinConfigs => _skinConfigs;

        public void Initialize(IUIFactory uiFactory, ISaveLoadSystem saveLoadSystem, StagesCounter stagesCounter)
        {
            _stagesCounter = stagesCounter;
            _uiFactory = uiFactory;
            _saveLoadSystem = saveLoadSystem;
            CurrentSkin = saveLoadSystem.Load(SaveLoadType.CurrentSkin);
        }

        public void CheckNewSkins()
        {
            for (int i = 0; i < _skinConfigs.Count; i++)
            {
                if (_stagesCounter.CurrentStage != 0 && _stagesCounter.CurrentStage == _skinConfigs[i].OpensAfterStage && _stagesCounter.CurrentStage >= _stagesCounter.MaxCompletedStage)
                {
                    Sprite sprite = _skinConfigs[i].KnifePrefab.GetComponentInChildren<SpriteRenderer>().sprite;
                    RectTransform window = _uiFactory.CreatNewSkinWindow(sprite);
                    StartUpDownAnimation(window);
                    StartDownUpAnimation(window);
                    DestroyNewSkinWindow();
                }
            }
        }

        public void ChangeSkin(int skinNumber)
        {
            CurrentSkin = skinNumber;
            _saveLoadSystem.Save(SaveLoadType.CurrentSkin, skinNumber);
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
            _uiFactory.DestroyUI(UIType.NewSkinWindow);
        }
    }
}