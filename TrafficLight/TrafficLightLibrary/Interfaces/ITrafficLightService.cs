using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;

namespace TrafficLight.Domain.Core.Interfaces
{
    public interface ITrafficLightService
    {
        List<Digit> GetNext();
        bool GiveAnswer(int answer);
    }
}
