using System;
using System.Collections.Generic;

namespace TrafficLight.Domain.Core
{
    public class DigitEngine
    {
        public bool CheckDigit(Digit digit, int value)
        {
           if(value >= Masks.Count)
               throw new ArgumentOutOfRangeException();

           return (Masks[value] ^ digit.Mask) == 0;
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
