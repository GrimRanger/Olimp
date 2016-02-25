using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.DigitGenerator
{
    public class DigitsGenerator
    {
        private readonly DigitEngine _digitEngine;

        public DigitsGenerator()
        {
            _digitEngine = new DigitEngine();
        }

        public List<List<Digit>> GenerateDigits(int number)
        {
            var result = new List<List<Digit>>();
            for (var i = number; i >= 0; --i)
            {
                var digits = _digitEngine.ToDigits(i);
                result.Add(digits);
            }

            return result;
        }
    }
}
