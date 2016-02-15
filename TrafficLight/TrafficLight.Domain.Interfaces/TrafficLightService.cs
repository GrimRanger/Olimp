using TrafficLight.Domain.Core;

namespace TrafficLight.Domain.Interfaces
{
    public interface ITrafficLightService
    {
        Digit GetNext();
        void GiveAnsert();
    }
}
