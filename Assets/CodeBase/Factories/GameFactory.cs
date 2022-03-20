using System.Collections.Generic;
using System.ComponentModel;
using CodeBase.Beam;
using CodeBase.Behaviours;
using CodeBase.Models;
using UnityEngine;

namespace CodeBase.Factories
{
    [CreateAssetMenu]
    public class GameFactory : ScriptableObject
    {
        [SerializeField]
        private BeamMotion[] _stages = new BeamMotion[5];
        [SerializeField, Range(0f,100f)] 
        private int _appleChance = 25;

        [Header("Pin object")]
        [SerializeField]
        private PinBehaviour _pinBehaviour;
        [SerializeField]
        private Model _appleModel;
        [SerializeField]
        private Model _knifeModel;

        [Header("Player object")]
        [SerializeField]
        private ThrowBehaviour _throwBehaviour;
        [SerializeField]
        private Model _playerKnifeModel;
        
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
            
            PinBehaviour apple = Instantiate(_pinBehaviour, _container.transform);
            Instantiate(_appleModel, apple.transform);
            SetPosition(apple.transform, _beam.transform);
            SetAngle(apple.transform, _beam.transform, _applePosition);
            Attach(apple);
        }

        public void CreateKnives()
        {
            int knivesCount = Random.Range(1, _maxKnife + 1);

            for (int i = 0; i < knivesCount; i++)
            {
                PinBehaviour knife = Instantiate(_pinBehaviour, _container.transform);
                Instantiate(_knifeModel, knife.transform);
                SetPosition(knife.transform, _beam.transform);
                SetAngle(knife.transform, _beam.transform, _knivesPositions[0]);
                Attach(knife);
                RemoveInListPositions();
            }
        }

        public void CreatePlayerKnife()
        {
            ThrowBehaviour knife = Instantiate(_throwBehaviour);
            Instantiate(_playerKnifeModel, knife.transform);
            SetPosition(knife.transform);
        }

        private void SetPosition(Transform entity) => 
            entity.position = new Vector3(0f, -4f, 0f);

        private void SetPosition(Transform entity, Transform beam) => 
            entity.position = beam.transform.position + new Vector3(0.7f, 0f, 0f);

        private void SetAngle(Transform entity, Transform beam, float angle) => 
            entity.transform.RotateAround(beam.transform.position, Vector3.forward, angle);

        private void Attach(PinBehaviour entity)
        {
            var attach = entity.GetComponent<Attacher>();
            var rb = _beam.GetComponent<Rigidbody>();
            attach.Attach(rb);
        }

        private void RemoveInListPositions()
        {
            _knivesPositions.RemoveAt(0);
            Debug.Log(_knivesPositions.Count);
        }

        private bool CheckAppleChance()
        {
            int random = Random.Range(1, 101);
            Debug.Log(random);
            return random <= _appleChance;
        }
    }
}