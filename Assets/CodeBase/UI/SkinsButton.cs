using CodeBase.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class SkinsButton : MonoBehaviour
    {
        [SerializeField]
        private Button _skinsButton;

        private IUIFactory _uiFactory;

        public void Initialize(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _skinsButton.onClick.AddListener(CreateSkinsScreen);
        }

        private void OnDisable() => 
            _skinsButton.onClick.RemoveListener(CreateSkinsScreen);

        private void CreateSkinsScreen() => 
            _uiFactory.CreateSkinsScreen();
    }
}