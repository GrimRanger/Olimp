using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Confirmit
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static void SolveTrafficLightProblem(ITrafficlight trafficLight)
        {
             bool[][] numbers = new bool[][] {
                new bool[] { true, true, true, false, true, true, true },
                new bool[] { false, false, true, false, false, true, false },
                new bool[] { true, false, true, true, true, false, true },
                new bool[] { true, false, true, true, false, true, true }, 
                new bool[] { false, true, true, true, false, true, false }, 
                new bool[] { true, true, false, true, false, true, true }, 
                new bool[] { true, true, false, true, true, true, true },
                new bool[] { true, false, true, false, false, true, false }, 
                new bool[] { true, true, true, true, true, true, true }, 
                new bool[] { true, true, true, true, false, true, true } };
            bool[] cellRepair1 = { false, false, false, false, false, false, false };
            bool[] cellRepair2 = { false, false, false, false, false, false, false };
            List<int> possiblePrevious = new List<int>();
            List<int> possibleCurrent = new List<int>();
            List<int> possibleFirst = new List<int>();
            List<int> forDel = new List<int>();
            bool first = true;
            while (trafficLight.GetNext())
            {
                for (int i = 0; i < 10; i++)
                {
                    bool check = true;
                    for (int j = 0; j < 7; j++)
                    {
                        if (trafficLight.Current.Item1[j])
                            cellRepair1[j] = true;
                        if (((trafficLight.Current.Item1[j]) && (!numbers[i][j])) || ((cellRepair1[j]) && (numbers[i][j]) && (!trafficLight.Current.Item1[j])))
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check)
                        possibleFirst.Add(i);
                }
                for (int i = 0; i < 10; i++)
                {
                    bool check = true;
                    for (int j = 0; j < 7; j++)
                    {
                        if (trafficLight.Current.Item2[j])
                            cellRepair2[j] = true;
                        if (((trafficLight.Current.Item2[j]) && (!numbers[i][j])) || ((cellRepair2[j]) && (numbers[i][j] && (!trafficLight.Current.Item2[j]))))
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check)
                        foreach (int item in possibleFirst)
                            possibleCurrent.Add(item * 10 + i);
                }
                possibleFirst.Clear();
                if (!first)
                {
                    foreach (int item in possibleCurrent)
                        if (!possiblePrevious.Contains(item + 1))
                            forDel.Add(item);
                    foreach (int item in forDel)
                        possibleCurrent.Remove(item);
                    forDel.Clear();
                    if (possibleCurrent.Count == 1)
                    {
                        trafficLight.Answer(possibleCurrent[0]);
                        break;
                    }
                    possiblePrevious.Clear();
                    foreach (var item in possibleCurrent)
                        possiblePrevious.Add(item);
                    possibleCurrent.Clear();
                }
                else
                {
                    possiblePrevious.Clear();
                    foreach (var item in possibleCurrent)
                        possiblePrevious.Add(item);
                    possibleCurrent.Clear();
                    first = false;
                }
            }
        }
    }
}
