using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core.Filters
{
    public class MaskDigitFilter : INumberFilter
    {
        private List<int> _digitWorkingLightMasks;
        private List<List<Digit>> _previousDigits;

        public MaskDigitFilter()
        {
            _previousDigits = new List<List<Digit>>();
        }

        public List<int> Filter(List<int> oldAnswers, int currentNumber, List<int> currentNumbers)
        {
            var newAnswers = new List<int>();
            for (var i = 0; i < oldAnswers.Count; ++i)
            {
                var index = currentNumbers.IndexOf(oldAnswers[i] - currentNumber);
                if (index != -1)
                    newAnswers.Add(oldAnswers[i]);
            }

            return newAnswers;
        }

        public bool CheckDigit(int expectedDigit, int actualDigit, int workingLine)
        {
            int workAndTurnOff = actualDigit ^ workingLine;
            int burned = expectedDigit ^ actualDigit;
            int conflict = workAndTurnOff & burned;

            return conflict == 0;
        }

        public void UpdateFilter(List<Digit> digits)
        {
            if (_digitWorkingLightMasks == null)
            {
                InitList(digits);
            }

            for (var i = 0; i < digits.Count; ++i)
            {
                _digitWorkingLightMasks[i] = (digits[i].Mask | _digitWorkingLightMasks[i]);
            }
        }

        private void InitList(List<Digit> digits)
        {
            _digitWorkingLightMasks = new List<int>();
            for (var i = 0; i < digits.Count; ++i)
                _digitWorkingLightMasks.Add(0);
        }
    }
}
