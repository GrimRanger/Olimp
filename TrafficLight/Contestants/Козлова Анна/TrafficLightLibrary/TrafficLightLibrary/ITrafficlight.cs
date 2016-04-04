using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLightLibrary
{
    public interface ITrafficLight
    {
        bool GetNext();

        Tuple<bool[], bool[]> Current { get; }

        void Answer(int value);
    }

}
