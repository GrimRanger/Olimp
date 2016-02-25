using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core.Filters
{
    public class SequenceDigitFilter : INumberFilter
    {
        public List<int> Filter(List<int> oldAnswers, int count, List<int> numbers)
        {
            var newAnswers = new List<int>();
            for (var i = 0; i < oldAnswers.Count; ++i)
            {
                var index = numbers.IndexOf(oldAnswers[i] - count);
                if (index != -1)
                    newAnswers.Add(oldAnswers[i]);
            }

            return newAnswers;
        }

        public void Update(List<List<Digit>> previousDigits)
        {
        }
    }
}
