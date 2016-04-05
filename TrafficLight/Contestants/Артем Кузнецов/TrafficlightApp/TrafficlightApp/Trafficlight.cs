using System;
using TrafficLight.Domain.Core.Interfaces;

namespace Artem_Kuznetsov
{
 
    public class Trafficlight : ITrafficLight
    {
        private const int CellsCount = 7;
        private int _number;
        private readonly Tuple<bool[], bool[]> _workingCells;
        private readonly ILogger _logger;

        public Tuple<bool[], bool[]> Current { get; private set; }

        public Trafficlight(int number, Tuple<bool[], bool[]> workingCells, ILogger logger)
        {
            if (number < 0)
                throw new ArgumentException("Number must be non-negative", "number");

            if (number > 99)
                throw new ArgumentException("Number must be less than 100", "number");

            if (workingCells == null)
                throw new ArgumentException("Working cells are not initialized", "workingCells");

            if (workingCells.Item1.Length != CellsCount || workingCells.Item2.Length != CellsCount)
                throw new ArgumentException("Count of working cells in each part should be " + CellsCount, "workingCells");

            if (logger == null)
                throw new ArgumentException("Logger is not initialized", "logger");

            _number = number;
            _workingCells = workingCells;
            _logger = logger;
        }

        public bool GetNext()
        { 
            if (_number == 0)
                return false;

            var leftNumber = GetTrafficlightNumber(_number / 10);
            var rightNumber = GetTrafficlightNumber(_number % 10);

            for (var i = 0; i < CellsCount; i++)
            {
                leftNumber[i] &= _workingCells.Item1[i];
                rightNumber[i] &= _workingCells.Item2[i];
            }

            Current = new Tuple< bool[], bool[]> (leftNumber, rightNumber);

            _number--;
            return true;
        }

        private static bool[] GetTrafficlightNumber(int number)
        {
            switch (number)
            {
                case 0:
                    return new[] {true, true, true, false, true, true, true};
                case 1:
                    return new[] {false, false, true, false, false, true, false};
                case 2:
                    return new[] {true, false, true, true, true, false, true};
                case 3:
                    return new[] {true, false, true, true, false, true, true};
                case 4:
                    return new[] {false, true, true, true, false, true, false};
                case 5:
                    return new[] {true, true, false, true, false, true, true};
                case 6:
                    return new[] {true, true, false, true, true, true, true};
                case 7:
                    return new[] {true, false, true, false, false, true, false};
                case 8:
                    return new[] {true, true, true, true, true, true, true};
                case 9:
                    return new[] {true, true, true, true, false, true, true};
                default:
                    throw new InvalidOperationException();
            }
        }

        public void Answer(int value)
        {
            _logger.Write(value.ToString());
        }
    }
}