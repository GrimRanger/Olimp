using System;
using System.Collections.Generic;

namespace TrafficLight.Domain.Core.Core
{
    public class DigitEngine
    {
        public bool CheckDigit(Digit digit, int value)
        {
            if (value >= Masks.Count)
                throw new ArgumentOutOfRangeException();

            return (Masks[value] | digit.Mask) == Masks[value];
        }

        public List<int> GetPossibleDigits(Digit digit)
        {
            var result = new List<int>();
            for (var i = 0; i < Masks.Count; ++i)
            {
                if ((Masks[i] | digit.Mask) == Masks[i])
                    result.Add(i);
            }

            return result;
        }

        public List<Digit> ToDigits(int number)
        {
            var result = new List<Digit>();
            if (number == 0)
            {
                result.Add(GenerateDigit(0));
                return result;
            }
            while (number > 0)
            {
                var digit = number % 10;
                number = number / 10;
                result.Add(GenerateDigit(digit));
            }
            result.Reverse();

            return result;
        }

        public List<Digit> ToDigits(int number, int digitCount)
        {
            var result = ToDigits(number);
            while (result.Count < digitCount)
            {
                result.Insert(0, GenerateDigit(0));
            }

            return result;
        }

        private Digit GenerateDigit(int digit)
        {
            var mask = Masks[digit];

            return new Digit(mask);
        }

        private static readonly List<int> Masks = new List<int>
			{
                119, //1110111 - 0
				18,  //0010010 - 1
				93,  //1011101 - 2
				91,  //1011011 - 3
				58,  //0111010 - 4
                107, //1101011 - 5
                111, //1101111 - 6
                82,  //1010010 - 7
                127, //1111111 - 8
                123  //1111011 - 9
			};
    }
}
