using System.Collections.Generic;
using CodeBase.Factories;
using CodeBase.Game.Counters;
using UnityEngine;

namespace CodeBase.UI.HUD
{
    public class KnivesHUD : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _knivesContainer;

        private UIFactory _uiFactory;
        private KnivesCounter _knivesCounter;
        private GameFactory _gameFactory;
        
        private readonly List<GameObject> _knivesList = new List<GameObject>();

        public void Initialize(UIFactory uiFactory, KnivesCounter knivesCounter, GameFactory gameFactory)
        {
            _uiFactory = uiFactory;
            _knivesCounter = knivesCounter;
            _gameFactory = gameFactory;

            _gameFactory.AttachedKnivesCreated += ClearAndCreateKnives;
            _knivesCounter.DecreaseNumberOfKnives += OnDecreaseNumberOfKnives;

        }

        private void OnDisable()
        {
            _gameFactory.AttachedKnivesCreated -= ClearAndCreateKnives;
            _knivesCounter.DecreaseNumberOfKnives -= OnDecreaseNumberOfKnives;
        }

        private void ClearAndCreateKnives()
        {
            ClearKnives();
            CreateKnives();
        }

        private void OnDecreaseNumberOfKnives(int number)
        {
            var item = _knivesList[number];
            var image = item.GetComponent<KnifeImage>().Knife;
            image.color = Color.gray;
        }

        private void ClearKnives()
        {
            foreach (var knife in _knivesList) 
                Destroy(knife.gameObject);

            _knivesList.Clear();
        }

        private void CreateKnives()
        {
            for (int i = 0; i < _knivesCounter.NumberOfKnives; i++)
            {
                var knife = _uiFactory.CreateKnife();
                knife.transform.SetParent(_knivesContainer);
                _knivesList.Add(knife);
            }
        }
    }
}