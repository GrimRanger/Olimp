using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core.Filters
{
    public class MaskDigitFilter : INumberFilter
    {
        private List<int> _digitWorkingLightMasks;
        private readonly DigitEngine _digitEngine;
        private List<List<Digit>> _previousDigits;

        public MaskDigitFilter()
        {
            _digitEngine = new DigitEngine();
        }

        public List<int> Filter(List<int> answers, int count, List<int> numbers)
        {
            var newAnswers = new List<int>();
            for (var i = 0; i < answers.Count; ++i)
            {
                var add = true;
                for (var j = 0; j < _previousDigits.Count; ++j)
                {
                    add = CheckNumber(answers[i] - j, _previousDigits[j]);
                    if (!add)
                        break;
                }
                if (add)
                    newAnswers.Add(answers[i]);
            }

            return newAnswers;
        }

        public void Update(List<List<Digit>> previousDigits)
        {
            _previousDigits = previousDigits;
            var numberCount = previousDigits.Count;

            if (_digitWorkingLightMasks == null)
            {
                InitList(previousDigits[0]);
            }

            for (var i = 0; i < previousDigits[numberCount - 1].Count; ++i)
            {
                _digitWorkingLightMasks[i] = (previousDigits[numberCount - 1][i].Mask | _digitWorkingLightMasks[i]);
            }
        }

        private bool CheckNumber(int number, List<Digit> actualDigits)
        {
            var index = 0;
            var expectedDigits = _digitEngine.ToDigits(number);
            if (expectedDigits.Count != actualDigits.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (expectedDigits.Count != _digitWorkingLightMasks.Count)
            {
                index = _digitWorkingLightMasks.Count - expectedDigits.Count;
            }

            for (var i = 0; i < actualDigits.Count; ++i)
            {
                if (!CheckDigit(expectedDigits[i].Mask, actualDigits[i].Mask, _digitWorkingLightMasks[index + i]))
                    return false;
            }

            return true;
        }

        private bool CheckDigit(int expectedDigit, int actualDigit, int workingLine)
        {
            int workAndTurnOff = actualDigit ^ workingLine;
            int burned = expectedDigit ^ actualDigit;
            int conflict = workAndTurnOff & burned;

            return conflict == 0;
        }

        private void InitList(List<Digit> digits)
        {
            _digitWorkingLightMasks = new List<int>();
            for (var i = 0; i < digits.Count; ++i)
                _digitWorkingLightMasks.Add(0);
        }
    }
}
