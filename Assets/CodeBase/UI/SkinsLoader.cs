using System;
using System.Collections.Generic;
using CodeBase.Factories;
using CodeBase.Game;
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
        private GameFactory _gameFactory;
        
        private readonly List<Button> _buttonsList = new List<Button>();

        public void Initialize(Skins skins, GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _skins = skins;
            LoadSkins();
        }

        private void OnDisable()
        {
            foreach (var button in _buttonsList) 
                button.onClick.RemoveAllListeners();
        }

        private void LoadSkins()
        {
            var defaultKnifePrefab = _skins.DefaultKnifePrefab;
            var defaultButton = Instantiate(_button, _container.transform);
            var defaultKnife = Instantiate(defaultKnifePrefab, defaultButton.transform);
            defaultKnife.transform.localScale *= 100;
            defaultButton.onClick.AddListener(delegate { OnClick(defaultKnifePrefab); });
            _buttonsList.Add(defaultButton);
            
            for (int i = 0; i < _skins.SkinConfigs.Count; i++)
            {
                var knifePrefab = _skins.SkinConfigs[i].KnifePrefab;
                var button = Instantiate(_button, _container.transform);
                var open = _skins.SkinConfigs[i].Open;

                if (!open) 
                    button.interactable = false;

                var knife = Instantiate(knifePrefab, button.transform);
                knife.transform.localScale *= 100;
                button.onClick.AddListener(delegate { OnClick(knifePrefab); });
                _buttonsList.Add(button);
            }
        }

        private void OnClick(GameObject knife) => 
            _gameFactory.ChangeKnifeSkin(knife);
    }
}