using System.Collections.Generic;
using CodeBase.Factories;
using CodeBase.ObjectType;

namespace CodeBase.Collection
{
    public class KnivesCollection
    {
        public List<Knife> KnivesList { get; } = new List<Knife>();
        
        private readonly IGameFactory _gameFactory;

        public KnivesCollection(IGameFactory gameFactory)
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