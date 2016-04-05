using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLight1
{
    class Program
    {
        static List<int> possibleN = new List<int>(); //числа, которые могут быть на табло в данный момент
        static bool[][] workMesh = new bool[2][] //true это ячейки табло, которые точно работают , а false это не понятно работают или нет
        {
            new bool[]{false, false, false, false, false, false, false,}, // первая цифра на табло
            new bool[]{false, false, false, false, false, false, false,}  // вторая цифра на табло
        };
        static bool[][] Number = new bool[10][]   //показывает как хранятся чила в виде ячеек
            {
                 //point      0     1     2     3     4     5     6    
                new bool[] { true, true, true,false, true, true, true}, //0 number
                new bool[] {false,false, true,false,false, true,false}, //1
                new bool[] { true,false, true, true, true,false, true}, //2
                new bool[] { true,false, true, true,false, true, true}, //3
                new bool[] {false, true, true, true,false, true,false}, //4
                new bool[] { true, true,false, true,false, true, true}, //5
                new bool[] { true, true,false, true, true, true, true}, //6
                new bool[] { true,false, true,false,false, true,false}, //7
                new bool[] { true, true, true, true, true, true, true}, //8
                new bool[] { true, true, true, true,false, true, true}  //9 number
            };

        public bool Belong(bool[] board, int canidate, int key)    //проверяем может ли число candidate быть на данной позиции (board) табло 
        {
            for (int i = 0; i < 7; i++)
            {
                if (board[i] == true)
                    workMesh[key][i] = true; //помечаем, что ячейка табло в принципе работает 
                if ((Number[canidate][i] == false) && ((board[i] == true) || (workMesh[key][i] == true)))
                    return false;
            }
            return true;
        }
        public void CreateCandidates(bool[] first, bool[] second)//записывает числа, которые возможны в самом первом состоянии табло
        {
            List<int> secondNumber = new List<int>();
            for( int i=0; i<10; i++)
                if(Belong(second,i,2))
                    secondNumber.Add(i);         //подбор возможных чисел на вторую позицию табло
            for (int i = 0; i < 10; i++)
                if (Belong(first, i,1))       //подбор возможных чисел на первую позицию табло
                    foreach (int j in secondNumber)
                        possibleN.Add(i * 10 + secondNumber[j]);  // создание возможных чисел для табло 

        }


        public void SolveTrafficLightProblem(ITrafficLight trafficLight)
        {

            CreateCandidates(trafficLight.Current.Item1, trafficLight.Current.Item1);
            while (possibleN.Count > 1)
            {
                if (trafficLight.GetNext())               
                for (int i = 0; i < possibleN.Count; i++)
                    {
                        possibleN[i]--;
                        if ((!Belong(trafficLight.Current.Item2, possibleN[i] % 10, 2)) || (!Belong(trafficLight.Current.Item1, possibleN[i] / 10, 1)))
                        {
                            possibleN.RemoveAt(i); //удаляем из списка возможных чисел число, которое нам не подходит
                            i--;
                        }
                    }

                else
                {
                    trafficLight.Answer(0);
                    return;
                }
            }
            trafficLight.Answer(possibleN[0]); 
        }
        static void Main(string[] args)
        {
        }
    }
}
