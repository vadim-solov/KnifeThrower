using CodeBase.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class HomeButton : MonoBehaviour
    {
        [SerializeField]
        private Button _homeButton;

        private IUIFactory _uiFactory;

        public void Initialize(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _homeButton.onClick.AddListener(GoHome);
        }
        
        private void OnDisable() => 
            _homeButton.onClick.RemoveListener(GoHome);

        private void GoHome()
        {
            _uiFactory.DestroyUI(UIType.HUD);
            _uiFactory.DestroyUI(UIType.LoseScreen);
            _uiFactory.CreateStartScreen();
        }
    }
}