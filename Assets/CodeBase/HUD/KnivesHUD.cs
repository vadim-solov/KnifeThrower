using System.Collections.Generic;
using CodeBase.Factories;
using CodeBase.Game;
using UnityEngine;

namespace CodeBase.HUD
{
    public class KnivesHUD : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _knivesContainer;

        private readonly List<GameObject> _knivesList = new List<GameObject>();

        private UIFactory _uiFactory;
        private VictoryController _victoryController;
        private KnivesCounter _knivesCounter;
        private RestarterController _restarterController;
        private GameFactory _gameFactory;

        public void Initialize(UIFactory uiFactory, KnivesCounter knivesCounter, VictoryController victoryController, RestarterController restarterController, GameFactory gameFactory)
        {
            _uiFactory = uiFactory;
            _knivesCounter = knivesCounter;
            _victoryController = victoryController;
            _restarterController = restarterController;
            _gameFactory = gameFactory;

            _gameFactory.AttachedKnivesCreated += ClearAndCreateKnives;
            _knivesCounter.DecreaseNumberOfKnives += OnDecreaseNumberOfKnives;

        }

        private void OnDisable()
        {
            _gameFactory.AttachedKnivesCreated -= ClearAndCreateKnives;
            _knivesCounter.DecreaseNumberOfKnives -= OnDecreaseNumberOfKnives;
        }

        private void OnDecreaseNumberOfKnives(int number)
        {
            var item = _knivesList[number];
            var image = item.GetComponent<KnifeImage>().Knife;
            image.color = Color.gray;
        }

        private void ClearAndCreateKnives()
        {
            ClearKnives();
            CreateKnives();
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