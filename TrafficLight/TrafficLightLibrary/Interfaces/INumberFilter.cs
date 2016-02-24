using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.Interfaces
{
    public interface INumberFilter
    {
        List<int> Filter(List<int> oldAnswers, int currentNumber, List<int> currentNumbers);
        void UpdateFilter(List<Digit> digits);
    }
}