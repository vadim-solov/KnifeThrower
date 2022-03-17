using CodeBase.Beam;
using UnityEngine;

namespace CodeBase.Factories
{
    [CreateAssetMenu]
    public class GameFactory : ScriptableObject
    {
        [SerializeField]
        private BeamMotion[] _stages = new BeamMotion[5];
        [SerializeField, Range(1,100)] 
        private int _appleChance = 25;
        [SerializeField]
        private int _knifeChance = 3;

        public void CreateBeam()
        {
            BeamMotion beam = Instantiate(_stages[0]);
            beam.StartRotation();
        }

        private void CreateApple()
        {
            
        }
    }
}