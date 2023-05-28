using System;

namespace Common.Score
{
    public class Score
    {
        public event Action<int> Changed; 

        private int _value;
        
        public void Add(int value)
        {
            _value += value;

            Changed?.Invoke(_value);
        }

        public void Remove(int value)
        {
            _value -= value;
            
            Changed?.Invoke(_value);
        }
    }
}