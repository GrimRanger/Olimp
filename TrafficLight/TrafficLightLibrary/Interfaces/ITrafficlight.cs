using System;

namespace TrafficLight.Domain.Core.Interfaces
{
    public interface ITrafficLight
    {
        bool GetNext();
        Tuple<bool[], bool[]> Current { get; }
        void Answer(int value);
    }
}
