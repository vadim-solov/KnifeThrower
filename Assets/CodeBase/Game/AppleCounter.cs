using System;
using CodeBase.SaveLoadSystem;
using UnityEngine;

namespace CodeBase.Game
{
    public class AppleCounter
    {
        private readonly ISaveLoadSystem _saveLoadSystem;

        public int Score { get; private set; } = 0;

        public event Action<int> ScoreChanged;

        public AppleCounter(ISaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
            Score = _saveLoadSystem.LoadApples();
        }

        public void IncreaseScore()
        {
            Score++;
            _saveLoadSystem.SaveApples(Score);
            ScoreChanged?.Invoke(Score);
        }
    }
}