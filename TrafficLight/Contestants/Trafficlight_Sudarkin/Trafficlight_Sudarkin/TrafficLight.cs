using System;
using System.Collections.Generic;
using System.Linq;

namespace Trafficlight_Sudarkin
{
    public class TrafficLight : ITrafficLight
    {
        private readonly Tuple<bool[], bool[]>[] _numbers;
        private int _position = -1;

        public Tuple<bool[], bool[]> Current => _numbers[_position];

        public TrafficLight(IEnumerable<Tuple<bool[], bool[]>> nums)
        {
            _numbers = nums.ToArray();
        }

        public bool GetNext()
        {
            _position++;
            return _position < _numbers.Length;
        }

        public void Answer(int value)
        {
            Console.WriteLine($"Светофор показывает {value}");
        }
    }
}
