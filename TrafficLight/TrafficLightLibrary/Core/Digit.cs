using System;
using System.Collections.Generic;

namespace TrafficLight.Domain.Core.Core
{
    public class Digit
    {
        public List<int> GlowingLines = new List<int>();

        public int Mask { get; private set; }

        public Digit(int mask)
        {
            Mask = mask;
          
            for (var i = 0; i < Lines.Masks.Count; ++i)
            {
                if ((Lines.Masks[i] ^ mask) > 0)
                    GlowingLines.Add(i);
            }
        }

        public string ToBinary()
        {
            var result = Convert.ToString(Mask, 2);

            return result;
        }
    }
}
