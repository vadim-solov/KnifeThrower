using CodeBase.SaveLoadSystem;
using UnityEngine;

namespace CodeBase.Game
{
    public class AppleCounter
    {
        private readonly ISaveLoadSystem _saveLoadSystem;
        
        private int _score = 0;

        public AppleCounter(ISaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
            _score = _saveLoadSystem.LoadApples();
        }

        public void IncreaseScore()
        {
            _score++;
            _saveLoadSystem.SaveApples(_score);
            Debug.Log("Apples now " + _saveLoadSystem.LoadApples());
        }
    }
}