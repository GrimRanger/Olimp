using System.Collections.Generic;
using System.Linq;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core.DigitReaders
{
    public class DigitStorageReader : IDigitReader
    {
        private readonly List<List<Digit>> _digits;
        private readonly int _rightAnswer;
        private int _step;

        public DigitStorageReader(List<List<Digit>> digits, int rightAnswer)
        {
            _digits = digits;
            _rightAnswer = rightAnswer;
            _step = 0;
        }

        public List<Digit> ReadDigits()
        {
            if (_digits.Count() > _step)
            {
                var temp = _digits[_step];
                _step++;
                return temp;
            }

            return null;
        }

        public int GetRightAnswer()
        {
            return _rightAnswer;
        }

        public int GetStep()
        {
            return _step;
        }

        public int GetLastNumber()
        {
            return _rightAnswer - _step + 1;
        }
    }
}