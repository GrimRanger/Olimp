using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexander_Efimov
{
    public class Alexander_Efimov
    {
        static private TrafficLight trafficlight;

        static private List<bool> oneVariants = new List<bool> { true, true, true, true, true, true, true, true, true, true };
        static private List<bool> twoVariants = new List<bool> { true, true, true, true, true, true, true, true, true, true };


        static void Main(string[] args)
        {
            //первый массив - битые ячейки к певрой цифре, второй массив - ко второй цифре
            trafficlight = new TrafficLight(75, new int[] { 4 }, new int[] { 3, 5 });
            SolveTrafficLightProblem();
        }


        public static  void SolveTrafficLightProblem()
        {
            while (trafficlight.GetNext() && checkCountVariants())
            {
                trafficlight.removeBedVariants(oneVariants, twoVariants);
                print();
                if (checkCountVariants())
                    trafficlight.moveVariants(oneVariants, twoVariants);
            }
            if(!checkCountVariants())
            {
                trafficlight.Answer(getResult()); 
            }
            else
            {
                Console.Write("\nОпределить значение светофора не удалось!");
                trafficlight.Answer(0); 
            }
        }

        /// <summary>
        /// Проверка можем ли мы назвать значение на светофоре
        /// </summary>
        /// <returns>false - значение светофора установленно, true - значение светофора не установленно</returns>
        static private bool checkCountVariants()
        {
            int countOne = 0;
            int countTwo = 0;
            for (int i = 0; i < 10; i++)
            {
                if (oneVariants[i] == true)
                    countOne++;
                if (twoVariants[i] == true)
                    countTwo++;
            }
            if (countOne == 1 && countTwo == 1)
                return false;
            else
                return true;
        }


        static private int getResult()
        {
            int result = 0;
            for(int i = 0; i < 10; i++)
            {
                if (oneVariants[i] == true)
                    result = result + i * 10;
                if (twoVariants[i] == true)
                    result = result + i;
            }
            return result;
        }


        static private void print()
        {
            Console.WriteLine("Первая цифра(варианты): ");
            for (int i = 0; i < 10; i++)
            {
                if (oneVariants[i] == true)
                {
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine("\nВторая цифра(варианты): ");
            for (int i = 0; i < 10; i++)
            {
                if (twoVariants[i] == true)
                {
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine("\n");
        }
    }
}
