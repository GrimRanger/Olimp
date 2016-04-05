using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core
{
    public class TrafficLightAnalyzer
    {
        private TrafficLight _trafficLight;
        private readonly DigitEngine _digitEngine;
        private readonly List<INumberFilter> _numberFilters;

        public TrafficLightAnalyzer( List<INumberFilter> numberFilters)
        {
            _digitEngine = new DigitEngine();
            _numberFilters = numberFilters;
        }

        private void Init(ITrafficLight trafficLight)
        {
            if (trafficLight == null)
            {
                throw new ArgumentNullException("trafficLight");
            }
            _trafficLight = trafficLight as TrafficLight;
        }

        public void Analyze(ITrafficLight trafficLight)
        {
            Init(trafficLight);
            var answers = new List<int>();
            var numbers = new List<List<Digit>>();
           
            var count = 0;

            while (_trafficLight.GetNext())
            {
                var digits = _trafficLight.CurrentDigits;
                
                numbers.Add(digits);
                UpdateFilters(numbers);
                count++;
                var currentPossibleNumbers = GetPossibleNumbers(digits);
                if (answers.Count == 0)
                    answers.AddRange(currentPossibleNumbers);
                else
                    answers = FilterAnwers(answers, count - 1, currentPossibleNumbers);

                if (answers.Count == 1)
                {
                    _trafficLight.Answer(answers[0] - count + 1);
                    return;
                }
            }

            _trafficLight.Answer(0);
        }

        private void UpdateFilters(List<List<Digit>> numbers)
        {
            foreach (var filter in _numberFilters)
            {
                filter.Update(numbers);
            }
        }

        private List<int> FilterAnwers(List<int> answers, int count, List<int> currentPossibleNumbers)
        {
            foreach (var filter in _numberFilters)
            {
                answers = filter.Filter(answers, count, currentPossibleNumbers);
            }

            return answers;
        }

        private List<int> GetPossibleNumbers(List<Digit> digits)
        {
            var result = new List<int> { 0 };

            foreach (var digit in digits)
            {
                List<int> possibleDigits = _digitEngine.GetPossibleDigits(digit);
                var newResult = new List<int>();

                foreach (var number in result)
                {
                    foreach (var possibleDigit in possibleDigits)
                    {
                        newResult.Add(number * 10 + possibleDigit);
                    }
                }
                result = newResult;
            }

            return result;
        }
    }
}
