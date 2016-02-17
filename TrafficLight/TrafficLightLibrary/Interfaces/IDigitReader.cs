using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.Interfaces
{
    public interface IDigitReader
    {
        List<Digit> ReadDigits();
        int GetRightAnsert();
        int GetStep();
        int GetLastNumber();
    }
}