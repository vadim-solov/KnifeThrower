using System;
using UnityEngine;

namespace CodeBase.Game
{
    public class KnivesCounter
    {
        private int _numberOfKnives;

        public event Action Victory;
        
        public KnivesCounter(int numberOfKnives)
        {
            _numberOfKnives = numberOfKnives;
            Debug.Log(_numberOfKnives);
        }

        public void Decrease()
        {
            _numberOfKnives--;
            Debug.Log(_numberOfKnives);
            
            if(_numberOfKnives <= 0)
                Victory?.Invoke();            
        }

        public bool CheckLastKnife()
        {
            if (_numberOfKnives <= 0)
                return true;
            
            return false;
        }

        public void Reset()
        {
            _numberOfKnives = 3;
        }
    }
}