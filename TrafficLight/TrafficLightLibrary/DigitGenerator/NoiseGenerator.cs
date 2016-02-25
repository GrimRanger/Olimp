using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.DigitGenerator
{
    public class NoiseGenerator
    {
        private readonly Random _random;

        public NoiseGenerator()
        {
            _random = new Random();
        }
        public List<List<Digit>> AddNoise(List<List<Digit>> numbers)
        {
            foreach (var number in numbers)
            {
                for (var i = 0; i < number.Count; ++i)
                {
                    number[i] = AddNoise(number[i]);
                }
            }

            return numbers;
        }

        public Digit AddNoise(Digit digit)
        {
            int maxValue = Convert.ToInt32("1111111", 2) + 1;
            int noiseMask = _random.Next(0, maxValue);
            digit = new Digit(digit.Mask & noiseMask);
           
            return digit;
        }
    }
}
