using UnityEngine;

namespace CodeBase.Knife
{
    public class KnifeInput : MonoBehaviour
    {
        [SerializeField]
        private KnifeMotion _knifeMotion;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _knifeMotion.StartMotion();
            }
        }
    }
}