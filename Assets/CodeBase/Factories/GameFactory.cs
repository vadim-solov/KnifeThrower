using System.Collections.Generic;
using CodeBase.Beam;
using CodeBase.Knife;
using UnityEngine;
using Motion = CodeBase.Beam.Motion;

namespace CodeBase.Factories
{
    [CreateAssetMenu]
    public class GameFactory : ScriptableObject
    {
        [SerializeField]
        private BeamMotion[] _stages = new BeamMotion[5];
        [SerializeField]
        private Motion _applePrefab;
        [SerializeField]
        private Motion _knifePrefab;
        [SerializeField, Range(0f,100f)] 
        private int _appleChance = 25;

        private readonly float _applePosition = 60f;
        private readonly List<float> _knivesPositions = new List<float>() {0f, 120f, 240f};
        
        private int _maxKnife = 3;
        private GameObject _container;
        private BeamMotion _beam;

        public void CreateContainer() => 
            _container = new GameObject();

        public void CreateBeam()
        {
            _beam = Instantiate(_stages[0], _container.transform);
            _beam.StartRotation();
        }

        public void CreateApple()
        {
            if(!CheckAppleChance())
                return;
            
            Motion apple = Instantiate(_applePrefab, _container.transform);
            apple.Initialize(_beam.transform);
            apple.SetAngle(_applePosition);
            
            var attach = apple.GetComponent<Attacher>();
            var rb = _beam.GetComponent<Rigidbody>();
            
            attach.Attach(rb);
        }

        public void CreateKnives()
        {
            int knivesCount = Random.Range(1, _maxKnife + 1);

            for (int i = 0; i < knivesCount; i++)
            {
                Motion knife = Instantiate(_knifePrefab, _container.transform);
                knife.Initialize(_beam.transform);
                knife.SetAngle(_knivesPositions[0]);
                knife.StartRotation();
                _knivesPositions.RemoveAt(0);
                Debug.Log(_knivesPositions.Count);
            }
        }

        private bool CheckAppleChance()
        {
            int random = Random.Range(1, 101);
            Debug.Log(random);
            return random <= _appleChance;
        }

        public void CreatePlayerKnife()
        {
            
        }
    }
}