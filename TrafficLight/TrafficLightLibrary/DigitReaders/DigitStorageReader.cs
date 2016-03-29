using System.Collections.Generic;
using System.Linq;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core.DigitReaders
{
    public class DigitStorageReader : IDigitReader
    {
        private readonly List<List<Digit>> _digits;
        private readonly int _firstNumber;
        private int _step;

        public DigitStorageReader(List<List<Digit>> digits, int firstNumber)
        {
            _digits = digits;
            _firstNumber = firstNumber;
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
            return _firstNumber - _step + 1;
        }

        public int GetStep()
        {
            return _step;
        }

        public int GetFirstNumber()
        {
            return _firstNumber;
        }

        public bool IsFinalState()
        {
            if (_firstNumber - _step == 0)
            {
                _step++;
                return true;
            }

            return false;
        }
    }
}