using CodeBase.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class BackButton : MonoBehaviour
    {
        [SerializeField]
        private Button _backButton;

        private IUIFactory _uiFactory;

        public void Initialize(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _backButton.onClick.AddListener(Back);
        }
        
        private void OnDisable() => 
            _backButton.onClick.RemoveListener(Back);

        private void Back() => 
            _uiFactory.DestroyUI(UIType.SkinsScreen);
    }
}