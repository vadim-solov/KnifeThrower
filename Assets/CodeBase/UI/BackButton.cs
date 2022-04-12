using CodeBase.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class BackButton : MonoBehaviour
    {
        [SerializeField]
        private Button _backButton;

        private UIFactory _uiFactory;

        public void Initialize(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _backButton.onClick.AddListener(Back);
        }
        
        private void OnDisable() => 
            _backButton.onClick.RemoveListener(Back);

        private void Back() => 
            _uiFactory.DestroySkinsScreen();
    }
}