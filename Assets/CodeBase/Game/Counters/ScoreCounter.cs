using System;
using CodeBase.SaveLoadSystem;

namespace CodeBase.Game.Counters
{
    public class ScoreCounter
    {
        private readonly ISaveLoadSystem _saveLoadSystem;
        public int Score { get; private set; } = 0;

        public event Action<int> ScoreChanged;

        public ScoreCounter(ISaveLoadSystem saveLoadSystem) => 
            _saveLoadSystem = saveLoadSystem;

        public void IncreaseScore()
        {
            Score++;
            ScoreChanged?.Invoke(Score);
        }

        public void ResetScore()
        {
            CheckMaxScore();
            Score = 0;
            ScoreChanged?.Invoke(Score);
        }

        private void CheckMaxScore()
        {
            int loadedScore = _saveLoadSystem.LoadScore();
            
            if (loadedScore < Score)
                _saveLoadSystem.SaveScore(Score);
        }
    }
}