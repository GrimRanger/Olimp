using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core.DigitReaders.MaskReader
{
    public class IntegerMaskReader : IMaskReader
    {
        public int GetMask(string str)
        {
            return int.Parse(str);
        }
    }
}