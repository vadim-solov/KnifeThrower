using UnityEngine;
using Motion = CodeBase.Behaviours.Motion;

namespace CodeBase.Knife
{
    public class KnifeInput : MonoBehaviour
    {
        [SerializeField]
        private Motion motion;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                motion.StartMotion();
            }
        }
    }
}