using CodeBase.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class HomeButton : MonoBehaviour
    {
        [SerializeField]
        private Button _homeButton;

        private UIFactory _uiFactory;

        public void Initialize(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _homeButton.onClick.AddListener(GoHome);
        }
        
        private void OnDisable() => 
            _homeButton.onClick.RemoveListener(GoHome);

        private void GoHome()
        {
            _uiFactory.DestroyHUD();
            _uiFactory.DestroyLoseScreen();
            _uiFactory.CreateStartScreen();
        }
    }
}