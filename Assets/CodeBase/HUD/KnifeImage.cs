using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.HUD
{
    public class KnifeImage : MonoBehaviour
    {
        [SerializeField]
        private Image _knife;

        public Image Knife => _knife;
    }
}