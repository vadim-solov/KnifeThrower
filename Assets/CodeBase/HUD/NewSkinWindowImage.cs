using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.HUD
{
    public class NewSkinWindowImage : MonoBehaviour
    {
        [SerializeField]
        private Image _newSkinImage;

        private void Start()
        {
            _newSkinImage.SetNativeSize();
            _newSkinImage.rectTransform.localScale = new Vector3(0.35f, 0.35f, 1f);
        }

        public void AddSprite(Sprite sprite) => 
            _newSkinImage.sprite = sprite;
    }
}