using System.Collections.Generic;
using CodeBase.Game;
using UnityEngine;

namespace CodeBase.HUD
{
    public class KnivesHUD : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _knivesContainer;
        [SerializeField]
        private GameObject _knifePrefab;

        private readonly List<GameObject> _knivesList = new List<GameObject>();

        private VictoryController _victoryController;
        private KnivesCounter _knivesCounter;

        public void Initialize(KnivesCounter knivesCounter, VictoryController victoryController)
        {
            _knivesCounter = knivesCounter;
            _knivesCounter.DecreaseNumberOfKnives += OnDecreaseNumberOfKnives;
            _victoryController = victoryController;
            _victoryController.Victory += OnVictory;
        }

        private void OnDisable()
        {
            _knivesCounter.DecreaseNumberOfKnives -= OnDecreaseNumberOfKnives;
            _victoryController.Victory -= OnVictory;
        }

        private void Start() => 
            CreateKnivesList();

        private void OnDecreaseNumberOfKnives(int number)
        {
            var item = _knivesList[number];
            var image = item.GetComponent<KnifeImage>().Knife;
            image.color = Color.gray;
        }

        private void OnVictory()
        {
            RestKnivesList();
            CreateKnivesList();
        }

        private void RestKnivesList()
        {
            foreach (var knife in _knivesList) 
                Destroy(knife.gameObject);

            _knivesList.Clear();
        }

        private void CreateKnivesList()
        {
            for (int i = 0; i < _knivesCounter.NumberOfKnives; i++)
            {
                var knife = Instantiate(_knifePrefab, _knivesContainer.transform);
                _knivesList.Add(knife);
            }
        }
    }
}