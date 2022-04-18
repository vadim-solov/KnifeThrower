using System;
using System.Collections.Generic;
using CodeBase.Factories;
using CodeBase.Game;
using CodeBase.Game.Counters;
using CodeBase.SaveLoadSystem;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace CodeBase.UI
{
    public class SkinsLoader : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _container;
        [SerializeField]
        private Button _button;

        private Skins _skins;
        private StagesCounter _stagesCounter;

        private readonly List<Button> _buttonsList = new List<Button>();

        public void Initialize(Skins skins, StagesCounter stagesCounter)
        {
            _skins = skins;
            _stagesCounter = stagesCounter;
            LoadSkins();
        }

        private void OnDisable()
        {
            foreach (var button in _buttonsList) 
                button.onClick.RemoveAllListeners();
        }

        private void LoadSkins()
        {
            for (int i = 0; i < _skins.SkinConfigs.Count; i++)
            {
                int skinNumber = i;
                GameObject knifePrefab = _skins.SkinConfigs[i].KnifePrefab;
                Button button = Instantiate(_button, _container.transform);

                if (_stagesCounter.MaxCompletedStage <= skinNumber && i != 0) 
                    button.interactable = false;

                var knife = Instantiate(knifePrefab, button.transform);
                knife.transform.localScale *= 100;
                button.onClick.AddListener(delegate { OnClick(skinNumber); });
                _buttonsList.Add(button);
            }
        }

        private void OnClick(int skinNumber){
            _skins.ChangeSkin(skinNumber);
        }
    }
}