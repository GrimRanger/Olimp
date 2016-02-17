using System;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core.DigitReaders.MaskReader
{
    public class BinaryMaskReader : IMaskReader
    {
        public int GetMask(string str)
        {
            return Convert.ToInt32(str, 2);
        }
    }
}
