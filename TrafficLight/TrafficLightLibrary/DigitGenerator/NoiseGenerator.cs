using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.DigitGenerator
{
    public class NoiseGenerator
    {
        private readonly Random _random;
        private readonly List<int> _noises;

        public NoiseGenerator(List<string> noises)
        {
            _noises = new List<int>();
            foreach (var noise in noises)
            {
                _noises.Add(Convert.ToInt32(noise, 2));
            }
        }

        public NoiseGenerator(List<int> noises)
        {
            _noises = noises;
        }

        public NoiseGenerator(int digitCount)
        {
            _noises = new List<int>();
            _random = new Random();
            for (var i = 0; i < digitCount; ++i)
            {
                _noises.Add(GenerateNoise());
            }
        }

        public List<List<Digit>> AddNoise(List<List<Digit>> numbers)
        {
            foreach (var number in numbers)
            {
                var index = 0;
                if (number.Count != _noises.Count)
                {
                    index = _noises.Count - number.Count;
                }

                for (var i = 0; i < number.Count; ++i)
                {
                    number[i] = new Digit(number[i].Mask & _noises[index + i]);
                }
            }

            return numbers;
        }

        private int GenerateNoise()
        {
            int maxValue = Convert.ToInt32("1111111", 2) + 1;
            return  _random.Next(0, maxValue);
            
        }
    }
}
