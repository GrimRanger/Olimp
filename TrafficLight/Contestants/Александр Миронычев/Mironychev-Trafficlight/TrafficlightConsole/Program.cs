using System;
using System.Collections.Generic;

namespace Alexander_Mironych
{
    class Program
    {
        private const int startValue = 63;
        private static bool[] firstWorking = {false, true, true, false, false, true, true};
        private static bool[] secondWorking = {true, true, false, true, true, true, false};

        /* Логика решения задания описана в классе StatusDeterminer
        В данном примере для вычисления вызывается специальный демонстрационный метод Demo(...);
        Ответом на олимпиадное задание является метод StatusDeterminer.Determine(ITrafficlight trafficlight);
        По завершении выполнения метода свойство trafficlight.answer заполнено значением текущего состояния */
        static void Main(string[] args)
        {
            Display display = new Display();
            Trafficlight trafficlight = new Trafficlight(firstWorking, secondWorking);

            trafficlight.StartCountDown(startValue+1); // +1 for initialization
            
            IEnumerator<List<int>> demo = StatusDeterminer.Demo(trafficlight);
            while (demo.MoveNext())
            {
                Console.Clear();
                display.Show(trafficlight.Current);

                Console.WriteLine();
                Console.Write("Возможные значения:  ");
                foreach (int possible in demo.Current)
                    Console.Write(possible + "  ");
                Console.WriteLine();

                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    return;
            }
        }
    }
}
