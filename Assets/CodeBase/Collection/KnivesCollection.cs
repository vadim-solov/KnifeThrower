using System.Collections.Generic;
using CodeBase.Factories;
using CodeBase.ObjectType;
using UnityEngine;

namespace CodeBase.Collection
{
    public class KnivesCollection
    {
        public List<Knife> KnivesList { get; } = new List<Knife>();
        
        private readonly GameFactory _gameFactory;

        public KnivesCollection(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _gameFactory.KnifeCreated += OnKnifeCreated;
        }

        public void Cleanup() => 
            _gameFactory.KnifeCreated -= OnKnifeCreated;

        public void Clear() => 
            KnivesList.Clear();

        private void OnKnifeCreated(Knife knife) => 
            KnivesList.Add(knife);
    }
}