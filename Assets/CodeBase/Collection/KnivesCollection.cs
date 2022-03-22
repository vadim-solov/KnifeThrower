using System.Collections.Generic;
using CodeBase.Factories;
using CodeBase.ObjectType;
using UnityEngine;

namespace CodeBase.Collection
{
    public class KnivesCollection
    {
        private readonly GameFactory _gameFactory;
        private readonly List<Knife> _knivesList = new List<Knife>();

        public List<Knife> KnivesList => _knivesList;

        public KnivesCollection(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _gameFactory.KnifeCreated += OnKnifeCreated;
        }

        public void Cleanup()
        {
            _gameFactory.KnifeCreated -= OnKnifeCreated;
            Debug.Log("desub");
        }

        public void Clear() => 
            _knivesList.Clear();

        private void OnKnifeCreated(Knife knife)
        {
            _knivesList.Add(knife);
            Debug.Log("Knife added: " + knife);
        }
    }
}