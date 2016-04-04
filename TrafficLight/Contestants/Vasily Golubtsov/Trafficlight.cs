using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace Vasily_Golubtsov
{
    class Trafficlight : ITrafficLight
    {
        int _curStateDiggit;
        List<int> _breakBitsLeft;
        List<int> _breakBitsRight;
        bool[,] originalStatesMX = new bool [10, 7] {
                            {true, true, true, false, true, true, true},        // 0
                            {false, false, true, false, false, true, false},    // 1
                            {true, false, true, true, true, false, true},       // 2
                            {true, false, true, true, false, true, true},       // 3
                            {false, true, true, true, false, true, false},      // 4
                            {true, true, false, true, false, true, true},       // 5
                            {true, true, false, true, true, true, true},        // 6
                            {true, false, true, true, true, true, false},       // 7
                            {true, true, true, true, true, true, true},         // 8
                            {true, true, true, true, false, true, true}         // 9
            };

        Tuple<bool[], bool[]> _curState = new Tuple<bool[], bool[]>(new bool[7], new bool[7]);
        public Trafficlight(int curStateDiggit, List<int> breakBitsLeft, List<int> breakBitsRight)
        {
            _breakBitsLeft = breakBitsLeft;
            _breakBitsRight = breakBitsRight;
            _curStateDiggit = curStateDiggit+1;
            for (int i = 0; i < 7; i++)
            {
                _curState.Item1[i] = true;
                _curState.Item2[i] = true;
            }
        }

        void breakBits(bool[] row, List<int> breakList)
        {
            foreach (int bit in breakList)
                row[bit] = false;
        }

        public bool GetNext()
        {
            _curStateDiggit--;
            int units = _curStateDiggit % 10;
            int dec = (_curStateDiggit - units) / 10;

            var decadeState = originalStatesMX.GetRow(dec);
            breakBits(decadeState, _breakBitsLeft);
            _curState.Item1.Set(decadeState);

            var unitsState = originalStatesMX.GetRow(units);
            breakBits(unitsState, _breakBitsRight);
            _curState.Item2.Set(unitsState);

            if (_curStateDiggit >= 0)
                return true;
            return false;
        }
        public Tuple<bool[], bool[]> Current 
        { 
            get
            {
                return _curState;
            }
        }
        public void Answer(int value)
        {
            int test = 0;
            test++;
            test--;
        }
    }
}
