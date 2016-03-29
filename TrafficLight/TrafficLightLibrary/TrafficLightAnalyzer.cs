using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core
{
    public class TrafficLightAnalyzer
    {
        private readonly ITrafficLightService _trafficLightService;
        private readonly DigitEngine _digitEngine;
        private readonly List<INumberFilter> _numberFilters;

        public TrafficLightAnalyzer(ITrafficLightService trafficLightService, List<INumberFilter> numberFilters)
        {
            if (trafficLightService == null)
            {
                throw new ArgumentNullException("trafficLightService");
            }

            _trafficLightService = trafficLightService;
            _digitEngine = new DigitEngine();
            _numberFilters = numberFilters;
        }

        public int Analyze()
        {
            var answers = new List<int>();
            var numbers = new List<List<Digit>>();
            var digits = _trafficLightService.GetNext();
            var count = 0;

            while (digits != null && digits.Count != 0)
            {
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
                    _trafficLightService.GiveAnswer(answers[0]);
                    return answers[0];
                }
                digits = _trafficLightService.GetNext();
            }

            _trafficLightService.GiveAnswer(count - 1);
            return count - 1;
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
