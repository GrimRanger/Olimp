using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLightLibrary;

namespace TrafficLightTest
{
     
    public class IncorrectTrafficLight : ITrafficLight
    {
        private readonly bool[][] _template1 = {new bool[]{true, true, true, false, true, true, true},//0
                                              new bool[]{false, false, true, false, false, true, false},//1
                                              new bool[]{true, false, true, true, true, false, true},//2
                                              new bool[]{true, false, true, true, false, true, true},//3
                                              new bool[]{false, true, true, true, false, true, false},//4
                                              new bool[]{true, true, false, true, false, true, true},//5                                              
                                              new bool[]{true, true, false, true, true, true, true},//6
                                              new bool[]{true, false, true, false, false, true, false},//7
                                              new bool[]{true, true, true, true, true, true, true},//8
                                              new bool[]{true, true, true, true, false, true, true},//9
                                             };
        private readonly bool[][] _template2 = {new bool[]{true, true, true, false, true, true, true},//0
                                              new bool[]{false, false, true, false, false, true, false},//1
                                              new bool[]{true, false, true, true, true, false, true},//2
                                              new bool[]{true, false, true, true, false, true, true},//3
                                              new bool[]{false, true, true, true, false, true, false},//4
                                              new bool[]{true, true, false, true, false, true, true},//5                                              
                                              new bool[]{true, true, false, true, true, true, true},//6
                                              new bool[]{true, false, true, false, false, true, false},//7
                                              new bool[]{true, true, true, true, true, true, true},//8
                                              new bool[]{true, true, true, true, false, true, true},//9
                                             };

        private int _answer;
        private int _value;
        private int _numberOfGetNextCalls;

        public IncorrectTrafficLight(int number, int[] wrongBits1, int[] wrongBits2 )
        {
            if (number > 99 || number < 0)
            {
                throw new ArgumentOutOfRangeException("We have 2-digit positive traffic light number!");
            }
            _value = number;
            foreach (var bit in wrongBits1)
            {
                if (bit >=0 && bit <= 9)
                {
                    foreach(var item in _template1)
                    {
                        item[bit] = false;
                    }
                }
            }
 
            foreach (var bit in wrongBits2)
            {
                if (bit >= 0 && bit <= 9)
                {
                    foreach (var item in _template2)
                    {
                        item[bit] = false;
                    }
                }
            }
        }

        public int GetAnswer
        {
            get { return _answer; }
        }

        public int NumberOfGetNextCalls
        {
            get { return _numberOfGetNextCalls; }
        }

        public bool GetNext()
        {
            _numberOfGetNextCalls++;
            if (Current == null)
            {
                Current = new Tuple<bool[], bool[]>(_template1[_value / 10], _template2[_value % 10]);
                return true;
            }
            if (_value-- == 0)
            {
                return false;
            } 
            Current = new Tuple<bool[], bool[]>(_template1[_value / 10], _template2[_value % 10]);
            return true;
        }

        public Tuple<bool[],bool[]> Current
        {
	        get; private set;
        }

        public void Answer(int value)
        {
            _answer = value;
        }
    }
}
