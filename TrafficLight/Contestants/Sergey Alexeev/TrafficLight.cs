using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace Sergey_Alexeev
{

    public class TrafficLight : ITrafficLight
    {
        private Tuple<bool[], bool[]> _current;
        private Tuple<bool[], bool[]> _brokenCells;
        private int _value;

        public TrafficLight(Tuple<bool[], bool[]> brokenCells, int startValue)
        {
            this._brokenCells = brokenCells;
            this._value = startValue;
        }


        public Tuple<bool[], bool[]> Current
        {
            get
            {
                return this._current;
            }
            set
            {
                this._current = value;
            }
        }

        public void Answer(int value)
        {
            Console.WriteLine("Сфетофор показывает: " + (value < 10 ? "0" : "") + "{0}", value);
        }

        public bool GetNext()
        {
            if (Current == null) //Первый запуск
            {
                Current = _value.ToTuple(_brokenCells);
                return true;
            }

            Current = (--_value).ToTuple(_brokenCells);

            return _value > 0;
        }
    }
}
