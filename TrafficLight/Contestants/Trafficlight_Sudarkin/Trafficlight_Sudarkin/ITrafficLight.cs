using System;

namespace Trafficlight_Sudarkin
{
    public interface ITrafficLight
    {
        bool GetNext();
        Tuple<bool[], bool[]> Current { get; }
        void Answer(int value);
    }
}
