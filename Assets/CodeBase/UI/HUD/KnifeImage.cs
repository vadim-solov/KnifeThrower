using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.HUD
{
    public class KnifeImage : MonoBehaviour
    {
        [SerializeField]
        private Image _knife;

        public Image Knife => _knife;
    }
}