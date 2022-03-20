using UnityEngine;

namespace CodeBase.Behaviours
{
    public class KnifeInput : MonoBehaviour
    {
        [SerializeField]
        private Motion _motion;

        public void Initialize(Motion motion)
        {
            _motion = motion;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _motion.StartMotion();
            }
        }
    }
}