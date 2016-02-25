using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.Interfaces
{
    public interface IDigitEngine
    {
        bool CheckDigit(Digit digit, int value);
        List<int> GetPossibleDigits(Digit digit);
    }
}