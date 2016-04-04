using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace Alexander_Efimov
{
    class TrafficLight : ITrafficLight
    {
        private int number;
        private List<bool[]> figures = new List<bool[]>();
        private bool[] brokenCellsOne = new bool[] { true, true, true, true, true, true, true };
        private bool[] brokenCellsTwo = new bool[] { true, true, true, true, true, true, true };

        public Tuple<bool[], bool[]> Current { get; private set; }


        public TrafficLight(int n, int[] one, int[] two)
        {
            createRightFigures();
            number = n;
            for (int i = 0; i < one.Length; i++)
                brokenCellsOne[one[i]] = false;
            for (int i = 0; i < two.Length; i++)
                brokenCellsTwo[two[i]] = false;
        }

        public bool GetNext()
        {
            Current = recordableBrokenTuple();
            if(number == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Отсеиваем неподходящие варианты цифр
        /// </summary>
        public void removeBedVariants(List<bool> oneVariants, List<bool> twoVariants)
        {
            for (int i = 0; i < 7; i++)
            {
                if (Current.Item1[i] == true)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (figures[j][i] == false)
                        {
                            oneVariants[j] = false;
                        }
                    }
                }
                if (Current.Item2[i] == true)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (figures[j][i] == false)
                        {
                            twoVariants[j] = false;
                        }
                    }
                }
            }
            number--;
        }

        /// <summary>
        /// По значению Number создаем кортеж и бьем ячейки
        /// </summary>
        /// <returns>Ьитый кортеж значения светофора</returns>
        private Tuple<bool[], bool[]> recordableBrokenTuple()
        {
            bool[] oneArray = figures[(int)number/10];
            bool[] twoArray = figures[(int)number % 10];
            for (int i = 0; i < 7; i++)
            {
                if (brokenCellsOne[i] == false)
                    oneArray[i] = false;
                if (brokenCellsTwo[i] == false)
                    twoArray[i] = false;
            }
            Tuple<bool[], bool[]> light = new Tuple<bool[], bool[]>(oneArray, twoArray);
            return light;
        }


        /// <summary>
        /// Сдвигаем значения массива на еденичку при уменьшении number
        /// </summary>
        public void moveVariants(List<bool> oneVariants, List<bool> twoVariants)
        {
            if (number % 10 == 9)
            {
                bool tempOne = oneVariants[0];
                for (int i = 1; i < 10; i++)
                {
                    oneVariants[i - 1] = oneVariants[i];
                }
                oneVariants[9] = tempOne;
            }
            bool tempTwo = twoVariants[0];
            for (int i = 1; i < 10; i++)
            {
                twoVariants[i - 1] = twoVariants[i];
            }
            twoVariants[9] = tempTwo;
        }


        public void Answer(int value)
        {
            Console.WriteLine("\nОтвет: " + value + "\n");
        }

        /// <summary>
        /// Цифры в правильной записи
        /// </summary>
        private void createRightFigures()
        {
            figures.Add(new bool[] { true, true, true, false, true, true, true });
            figures.Add(new bool[] { false, false, true, false, false, true, false });
            figures.Add(new bool[] { true, false, true, true,  true, false, true});
            figures.Add(new bool[] { true, false, true, true, false, true, true});
            figures.Add(new bool[] { false, true, true, true, false, true, false});
            figures.Add(new bool[] { true, true, false, true, false, true, true});
            figures.Add(new bool[] { true, true, false, true, true, true, true });
            figures.Add(new bool[] { true, false, true, false, false, true, false});
            figures.Add(new bool[] { true, true, true, true, true, true, true });
            figures.Add(new bool[] { true, true, true, true, false, true, true });
        }
    }
}
