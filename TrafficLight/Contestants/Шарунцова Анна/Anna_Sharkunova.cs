using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrafficLight.Domain.Core.Interfaces;


namespace Anna_Sharkunova
{
    public class Anna_Sharkunova
    {
        Numbers numbers = new Numbers();
        
        public void SolveTrafficLightProblem(ITrafficLight itrafficlight)
        {
            int count;
            int rightNumber, leftNumber;
            List<int> currentPossibleLeft = new List<int>(); //хранит возможные цифры на данном шаге
            List<int> currentPossibleRight = new List<int>();
            List<int> nextPossibleLeft = new List<int>(); //хранит возможные цифры при следующем шаге
            List<int> nextPossibleRight = new List<int>();
            bool LeftNumberChanged = false;

            if (itrafficlight.GetNext())
            {
                //записываем возможные варианты цифр
                currentPossibleLeft = GetPossibleNumbers(itrafficlight.Current.Item1);
                currentPossibleRight = GetPossibleNumbers(itrafficlight.Current.Item2);

                //если вариантов правой цифры больше, чем 1
                while (currentPossibleRight.Count > 1)
                {
                    if (itrafficlight.GetNext())
                    {
                        nextPossibleLeft = GetPossibleNumbers(itrafficlight.Current.Item1);
                        nextPossibleRight = GetPossibleNumbers(itrafficlight.Current.Item2);
                        //если поменялась левая цифра
                        if (nextPossibleLeft != currentPossibleLeft)
                        {
                            LeftNumberChanged = true;
                            currentPossibleLeft = nextPossibleLeft;
                            break;
                        }
                        //сравниваем возможные варианты на предыдущем шаге и на этом
                        currentPossibleRight = ComparingPossibleLists(currentPossibleRight, nextPossibleRight);
                        rightNumber = currentPossibleRight.ElementAt(0);
                    }
                    else
                    {
                        itrafficlight.Answer(0);
                        return;
                    }

                }

                rightNumber = currentPossibleRight.ElementAt(0);
                //если левая цифра изменилась
                if (LeftNumberChanged)
                {
                    //то правая автоматически 9, а левая изменится только через 10 шагов
                    rightNumber = 9;
                    count = 10;
                }
                //если нет, то столько шагов ло изменения левой цифры        
                else count = rightNumber+1;

                while (currentPossibleLeft.Count > 1)
                {
                    while (count > 0)
                    {
                        //делаем эти шаги, если это возможно
                        if (itrafficlight.GetNext())
                            count--;
                        else
                        {
                            itrafficlight.Answer(0);
                            return;
                        }
                    }

                    nextPossibleLeft = GetPossibleNumbers(itrafficlight.Current.Item1);
                    currentPossibleLeft = ComparingPossibleLists(currentPossibleLeft, nextPossibleLeft);
                    count = 10;
                    rightNumber = 9;
                }

                leftNumber = currentPossibleLeft.ElementAt(0);
                itrafficlight.Answer(leftNumber * 10 + rightNumber);
            }
            else
            {
                itrafficlight.Answer(0);
                return;
            }
        }
        //Возвращает список цифр, возможных при таких горящих ячейках
        public List<int> GetPossibleNumbers(bool[] CurrentCells)
        {
            List<int> possibleNumbers = new List<int>();
            for (int j = 0; j < 10; j++)
            {
                bool IsPossible = true;
                for (int i = 0; i < 6; i++)
                {
                    if (CurrentCells[i] == true)
                    {
                        if (numbers.LightCells[j, i] == false)
                        {
                            IsPossible = false;
                            break;
                        }
                    }
                }
                if (IsPossible)
                    possibleNumbers.Add(j); //добавляем цифру, возможную при таких светящихся ячейках
            }
            return possibleNumbers;
        }
        //Сравнивает список возможных на предыдущем шаге и на этом, оставляет только те, которые могут идти подряд
        public List<int> ComparingPossibleLists(List<int> current, List<int> next)
        {
            foreach (int number in current)
            {
                if (!next.Contains(number - 1))
                    current.Remove(number);
            }
            return current;
        }
    }
    class Numbers
    {
        //массив, где числа от 0 до 9 хранят ячейки, которые должны гореть
        public readonly bool[,] LightCells;
        public Numbers()
        {
            LightCells = new bool[10, 7]{{ true, true, true, false, true, true, true },{false, false, true, false, false, true, false},
                {true, false, true, true, true, false, true}, {true, false, true, true, false, true, true},
                {false, true, true, true, false, true, false}, {true, true, false, true, false, true, true}, 
                {true, true, false, true, true, true, true}, {true, false, true, false, false, true, false},
                {true, true, true, true, true, true, true}, {true, true, true, true, false, true, true}};
        }
    }


}