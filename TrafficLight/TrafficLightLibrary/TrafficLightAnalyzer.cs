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
        private readonly List<INumberFilter> _numberFilters; 

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
            var answers = new List<int>();

            var digits = _trafficLightService.GetNext();
          
            var digitWorkingLightMasks = new List<int>(digits.Count);
            digitWorkingLightMasks = GetDigitsMask(digitWorkingLightMasks, digits);
          
            var count = 0;
            while (digits != null && digits.Count != 0)
            {
                var currentPossibleNumbers = GetPossibleNumbers(digits);
                if (answers.Count == 0)
                    answers.AddRange(currentPossibleNumbers);
                else
                    answers = GetNewPossibleAnswers(answers, count, currentPossibleNumbers);

                if (answers.Count == 1)
                {
                    _trafficLightService.GiveAnswer(answers[0]);
                    return answers[0];
                }
                digits = _trafficLightService.GetNext();
                digitWorkingLightMasks = GetDigitsMask(digitWorkingLightMasks, digits);
                count++;
            }

            _trafficLightService.GiveAnswer(count);
            return count;
        }

        private List<int> GetDigitsMask(List<int> digitWorkingLightMasks, List<Digit> digits)
        {
            for (var i = 0; i < digits.Count; ++i)
            {
                digitWorkingLightMasks[i] = (digits[i].Mask | digitWorkingLightMasks[i]);
            }

            return digitWorkingLightMasks;
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

        private List<int> GetNewPossibleAnswers(List<int> oldAnswers, int currentNumber, List<int> currentNumbers)
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

        //private List<int> GetNewPossibleAnswersByMask(List<int> oldAnswers, int currentNumber, List<int> currentNumbers)
        //{
          
        //}
    }
}
