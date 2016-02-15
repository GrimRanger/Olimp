using System;

namespace TrafficLight.Domain.Core.Interfaces
{
    public interface ITrafficLightService
    {
        Tuple<Digit, Digit> GetNext();
        void GiveAnsert(int answer);
    }
}
