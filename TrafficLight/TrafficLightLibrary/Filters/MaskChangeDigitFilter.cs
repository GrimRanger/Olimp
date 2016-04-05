using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core.Filters
{
    public class MaskChangeDigitFilter : INumberFilter
    {
        private List<List<Digit>> _previousDigits;

        public List<int> Filter(List<int> answers, int count, List<int> numbers)
        {
            var newAnswers = new List<int>();
            var i = 0;
            var cnt = 0;
            var tmp = (int)Math.Pow(10, _previousDigits[0].Count - 1);
            while (i < _previousDigits.Count - 1 && (_previousDigits[i][0].Mask == _previousDigits[i + 1][0].Mask))
            {
                cnt++;
                i++;
            }
            if (cnt > tmp || (i == _previousDigits.Count - 1) || (i < _previousDigits.Count - 1 && _previousDigits[i][0].Mask == _previousDigits[i + 1][0].Mask))
                return answers;

            foreach (var answer in answers)
            {
                if (answer % tmp == cnt)
                    newAnswers.Add(answer);
            }

            return newAnswers;
        }

        public void Update(List<List<Digit>> previousDigits)
        {
            _previousDigits = previousDigits;
        }
    }
}