using System;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.Interfaces
{
    public interface ITrafficLightService
    {
        Tuple<Digit, Digit> GetNext();
        void GiveAnswer(int answer);
    }
}
