using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.Interfaces
{
    public interface INumberFilter
    {
        List<int> Filter(List<int> answers, int count, List<int> numbers);
        void Update(List<List<Digit>> previousDigits);
    }
}