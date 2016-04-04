using System;
using TrafficLight.Domain.Core.Interfaces;

namespace Irina_Koroleva
{

    public class TrafficLight : ITrafficLight //класс для тестов. В данном случае не работают ячейки 0, 3 и 6 на каждой половине
    {
        private int countdown;
        private int timesOfCallingGetNext = 0;

        public Tuple<bool[], bool[]> Current { get; private set; }

        public void Answer(int value)
        {
            Console.WriteLine("The traffic light is showing {0}", value);
            Console.WriteLine("The \"GetNext\" method was called {0} times.", timesOfCallingGetNext);
        }

        public bool GetNext()
        {
            bool ret = true;
            if (Current == null)
            {
                Console.WriteLine("Start value: ");
                countdown = int.Parse(Console.ReadLine()); //поскольку в задании указано, что светофор заведомо правильный, проверки на введенное начальное значение нет
                countdown++;
            }
            countdown--;
            bool[] array1 = numberToCurrentState(countdown / 10);
            bool[] array2 = numberToCurrentState(countdown % 10);

            Current = new Tuple<bool[], bool[]>(array1, array2);

            if (countdown == 0)
            {
                ret = false;
            }
            else
            {
                ret = true;
            }

            timesOfCallingGetNext++;
            return ret;
        }

        private bool[] numberToCurrentState(int n)
        {
            bool[] result = new bool[] { false, false, false, false, false, false, false };

            switch (n)
            {
                case 9:
                    result = new bool[] { false, true, true, false, false, true, false };
                    break;
                case 8:
                    result = new bool[] { false, true, true, false, true, true, false };
                    break;
                case 7:
                    result = new bool[] { false, false, true, false, false, true, false };
                    break;
                case 6:
                    result = new bool[] { false, true, false, false, true, true, false };
                    break;
                case 5:
                    result = new bool[] { false, true, false, false, false, true, false };
                    break;
                case 4:
                    result = new bool[] { false, true, true, false, false, true, false };
                    break;
                case 3:
                    result = new bool[] { false, false, true, false, false, true, false };
                    break;
                case 2:
                    result = new bool[] { false, false, true, false, true, false, false };
                    break;
                case 1:
                    result = new bool[] { false, false, true, false, false, true, false };
                    break;
                case 0:
                    result = new bool[] { false, true, true, false, true, true, false };
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
