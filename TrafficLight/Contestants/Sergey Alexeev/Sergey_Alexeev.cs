using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace Sergey_Alexeev
{
    public class Sergey_Alexeev
    {
        //static void Main(string[] args)
        //{
        //    var rand = new Random(DateTime.Now.Millisecond);

        //    //Случайным образом определяем начало отсчета
        //    var startValue = rand.Next(100);

        //    //Также случайным образом - битые ячейки
        //    var leftBrokenCells = new bool[7];
        //    var rightBrokenCells = new bool[7];

        //    for(var i = 0; i < 7; i++)
        //    {
        //        leftBrokenCells[i] = rand.Next(-30, 10) > -1;
        //        rightBrokenCells[i] = rand.Next(-30, 10) > -1;
        //    }

        //    //Создаем экземпляр класса TrafficLight, реализующего ITrafficLight
        //    var trafficLight = new TrafficLight(new Tuple<bool[], bool[]>(leftBrokenCells, rightBrokenCells), startValue);

        //    SolveTrafficLightProblem(trafficLight);
        //}


        /// <summary>
        /// Убираем заведомо неверные варианты. Возвращаем возможные значения сфетофора учитывая те, что получили на прошлом шаге
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        static List<int> GetPossibleNumbers(bool[] cells, List<int> prevPossibleNumbers)
        {
            var result = new List<int>();

            for(var i = 0; i < prevPossibleNumbers.Count; i++)
            {
                var numeral = prevPossibleNumbers[i];
                var j = 0;
                for(; j < cells.Length; j++)
                {
                    //Если находим хотя бы одну ячейку, которая горит, но гореть не должна (если на табло та цифра, которую мы предполагаем) - исключаем эту цифру
                    if (cells[j] && !Helpers.numeralsFromInt[numeral][j])
                        break;
                }
                if (j == cells.Length) //Иначе добавляем ее в "возможные"
                    result.Add(numeral);
            }

            return result;
        }


        /// <summary>
        /// Решение задачи
        /// </summary>
        /// <param name="trafficLight"></param>
        public static void SolveTrafficLightProblem(ITrafficLight trafficLight)
        {
            int result = 0, stepsCount = 0;

            //Изначально табло может показывать любое число
            var leftPossibleNumerals = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var rightPossibleNumerals = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };         

            while (trafficLight.GetNext())
            {
                stepsCount++;

                //изначально так как мы начинаем искать левую цифру параллельно с правой, то может случиться следующая ситуация:
                //мы нашли возможных кандидатов для левой цифры, но не успели найти правую до того момента, как реальная левая
                //цифра на табло уменьшилась на 1. Тогда ее может уже не оказаться в списке кандидатов. В итоге ответ будет неверным.
                //В качестве решения можно начать искать левую цифру после того, как найдем правую. Но тогда в некоторых
                //случаях мы делаем лишний шаг (а мб и несколько).
                //Можно обработать эту ситуацию, добавив в список кандидатов доп. значения, уменьшенные на 1.
                //Этого хватит, т.к. правую цифру точно возможно определить за 10 итераций (если, конечно, это вообще возможно)

                if (leftPossibleNumerals.Count > 1)
                {
                    leftPossibleNumerals = GetPossibleNumbers(trafficLight.Current.Item1, leftPossibleNumerals);
                    if (rightPossibleNumerals.Count > 1)
                    {
                        var count = leftPossibleNumerals.Count;
                        for (var i = 0; i < count; i++)
                            if (leftPossibleNumerals[i] - 1 >= 0)
                                leftPossibleNumerals.Add(leftPossibleNumerals[i] - 1);
                        leftPossibleNumerals = leftPossibleNumerals.Distinct().ToList();
                    }
                }

                //Для правой цифры
                if (rightPossibleNumerals.Count > 1)
                    rightPossibleNumerals = GetPossibleNumbers(trafficLight.Current.Item2, rightPossibleNumerals);

                //Нашли ответ
                if (leftPossibleNumerals.Count == 1 && rightPossibleNumerals.Count == 1)
                {
                    result = leftPossibleNumerals[0] * 10 + rightPossibleNumerals[0];
                    break;
                }

                //Значение правой цифры всегда уменьшается на 1
                rightPossibleNumerals = rightPossibleNumerals.Select(x => --x < 0 ? 9 : x).ToList();

                //Как только нашли правую цифру - мы точно знаем, когда значение левой должно уменьшиться на 1
                if (rightPossibleNumerals.Count == 1 && rightPossibleNumerals[0] == 9)
                {
                    leftPossibleNumerals = leftPossibleNumerals.Select(x => --x).ToList();
                    leftPossibleNumerals.RemoveAll(x => x == -1);
                }               
            }

            stepsCount--;

            trafficLight.Answer(result); // вызов метода с текущим значением светофора
            Console.WriteLine("Потребовалось {0} шагов.", stepsCount);
        }
    }
}
