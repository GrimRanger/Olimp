using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core
{
    public class TrafficLightAnalyzer
    {
        private readonly ITrafficLightService _trafficLightService;
        private readonly IDigitEngine _digitEngine;

        public TrafficLightAnalyzer(ITrafficLightService trafficLightService, IDigitEngine digitEngine)
        {
            if (trafficLightService == null)
            {
                throw new ArgumentNullException("trafficLightService");
            }

            _trafficLightService = trafficLightService;
            _digitEngine = digitEngine;
        }

        public int Analyze()
        {
            var possibleAnswers = new List<int>();

            var digits = _trafficLightService.GetNext();
            var currentNumber = 0;
            while (digits != null && digits.Count != 0)
            {
                var currentPossibleNumbers = GetPossibleNumbers(digits);
                if (possibleAnswers.Count == 0)
                    possibleAnswers.AddRange(currentPossibleNumbers);
                else
                {
                    var newPossibleAnswers = new List<int>();
                    for (var i = 0; i < possibleAnswers.Count; ++i)
                    {
                        var index = currentPossibleNumbers.IndexOf(possibleAnswers[i] - currentNumber);
                        if (index != -1)
                            newPossibleAnswers.Add(possibleAnswers[i]);
                    }
                    possibleAnswers = newPossibleAnswers;
                }

                if (possibleAnswers.Count == 1)
                {
                    _trafficLightService.GiveAnswer(possibleAnswers[0]);
                    return possibleAnswers[0];
                }
                digits = _trafficLightService.GetNext();
                currentNumber++;
            }

            _trafficLightService.GiveAnswer(currentNumber);
            return currentNumber;
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
