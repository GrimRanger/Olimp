using System;

namespace TrafficLight.Domain.Core.Core
{
    public class Digit
    {
        public int Mask { get; private set; }

        public Digit(int mask)
        {
            Mask = mask;
        }

        public string ToBinary()
        {
            var result = Convert.ToString(Mask, 2);

            return result;
        }
    }
}
