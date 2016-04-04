using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trafficlight
{
    public interface ITrafficlight
    {
        bool GetNext();
        Tuple<bool[], bool[]> Current { get; }
        void Answer(int value);
    }
}
