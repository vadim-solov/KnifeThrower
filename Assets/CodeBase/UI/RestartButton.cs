using CodeBase.Game;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] 
        private Button _restartButton;

        private RestarterController _restarterController;
        
        public void Initialize(RestarterController restarterController)
        {
            _restarterController = restarterController;
            _restartButton.onClick.AddListener(OnClick);
        }

        private void OnDisable() => 
            _restartButton.onClick.RemoveListener(OnClick);

        private void OnClick() => 
            _restarterController.Restart();
    }
}