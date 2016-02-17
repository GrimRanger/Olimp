using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.DigitGenerator
{
    public class DigitsGenerator
    {
        public List<List<Digit>> GenerateDigits(int number)
        {
            var result = new List<List<Digit>>();
            for (var i = number; i >= 0; --i)
            {
                var digits = GenerateNumber(i);
                result.Add(digits);
            }

            return result;
        }

        private List<Digit> GenerateNumber(int number)
        {
            var result = new List<Digit>();
            while (number > 0)
            {
                var digit = number%10;
                number = number/10;
                result.Add(GenerateDigit(digit));
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
