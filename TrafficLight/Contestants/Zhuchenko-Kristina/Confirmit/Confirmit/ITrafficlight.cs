using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Confirmit
{
    public interface ITrafficlight
    {
        bool GetNext();
        Tuple<bool[], bool[]> Current { get; }
        void Answer(int value);
    }
}
