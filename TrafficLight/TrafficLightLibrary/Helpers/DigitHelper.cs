using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.Helpers
{
    public class DigitHelper
    {
        private readonly DigitEngine _digitEngine;

        public DigitHelper()
        {
            _digitEngine = new DigitEngine();
        }

        public bool[] ToBool(Digit digit)
        {
            var result = new bool[7];
            var mask = Convert.ToInt32("1000000", 2);
            for (var i = 0; i < 7; ++i)
            {
                result[i] = (digit.Mask & mask) != 0;
                mask = mask >> 1;
            }

            return result;
        }

        public Tuple<bool[], bool[]> ToBool(List<Digit> digits)
        {
            if (digits.Count != 2)
            {
                //throw new ArgumentOutOfRangeException("digits");
                return null;
            }

            var firstDigit = ToBool(digits[0]);
            var secondDigit = ToBool(digits[1]);

            return new Tuple<bool[], bool[]>(firstDigit, secondDigit);
        }

        public Tuple<bool[], bool[]> ToBool(int number)
        {
            if (number >= 100)
            {
                //throw new ArgumentOutOfRangeException("number");
                return null;
            }
            return ToBool(_digitEngine.ToDigits(number));
        }
    }
}
