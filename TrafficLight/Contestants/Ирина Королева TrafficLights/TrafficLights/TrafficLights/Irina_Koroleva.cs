using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace Irina_Koroleva
{
    public class Irina_Koroleva
    {
        public static string[] codesForConversion = new string[] { "1110111", "0010010", "1011101", "1011011", "0111010", "1101011", "1101111", "1010010", "1111111", "1111011" }; //состояния одной половины светофора от 0 до 9
        public static bool[] workingCells = new bool[] { false, false, false, false, false, false, false }; //если в какой-то момент ячейка была активна, отмечается как рабочая

        public static void SolveTrafficLightProblem(ITrafficLight trafficLight) //зависит от дополнительного метода WhatNumberThisPossiblyIs (далее)
        {
            int result;
            List<List<int>> chainsOfPossibilities = new List<List<int>>();

            bool countdownIsNotOver = trafficLight.GetNext();
            while (countdownIsNotOver)
            {
                List<int> possibleFirstNumber = WhatNumberThisPossiblyIs(trafficLight.Current.Item1);
                List<int> possibleSecondNumber = WhatNumberThisPossiblyIs(trafficLight.Current.Item2);

                List<int> possibleNumber = new List<int>();

                foreach (int i in possibleFirstNumber)
                {
                    foreach (int j in possibleSecondNumber)
                    {
                        possibleNumber.Add(i * 10 + j);
                    }
                }

                if (chainsOfPossibilities == null || chainsOfPossibilities.Count == 0)
                {
                    for (int i = 0; i < possibleNumber.Count; i++)
                    {
                        chainsOfPossibilities.Add(new List<int>() { possibleNumber.ElementAt(i) });
                    }
                }
                else
                {
                    foreach (List<int> l in chainsOfPossibilities)
                    {
                        int check = 101;
                        foreach (int n in possibleNumber)
                        {
                            if (l.Last() == n + 1)
                            {
                                check = n;
                                break;
                            }
                        }
                        l.Add(check);
                    }

                    List<List<int>> newChainsOfPossibilities = new List<List<int>>();
                    for (int i = 0; i < chainsOfPossibilities.Count; i++)
                    {
                        if (chainsOfPossibilities.ElementAt(i).Last() != 101)
                        {
                            newChainsOfPossibilities.Add(chainsOfPossibilities.ElementAt(i));
                        }
                    }
                    chainsOfPossibilities = newChainsOfPossibilities;

                    if (chainsOfPossibilities.Count == 1)
                    {
                        break;
                    }
                }
                countdownIsNotOver = trafficLight.GetNext();
            }

            if (!countdownIsNotOver)
            {
                result = 0;
            }
            else
            {
                result = chainsOfPossibilities.ElementAt(0).Last();
            }

            trafficLight.Answer(result); // вызов метода с текущим значением светофора
        }

        public static List<int> WhatNumberThisPossiblyIs(bool[] curr) //возвращает все возможные варианты того, какую цифру представляет ячейка с учетом ее предыдущих состояний
        {
            List<int> possibleMatches = new List<int>();
            string prepareRegexp = String.Empty;

            for (int i = 0; i < 7; i++)
            {
                if (curr[i] == true)
                {
                    prepareRegexp += "1";
                    workingCells[i] = true;
                }
                else
                {
                    if (workingCells[i] == true)
                    {
                        prepareRegexp += "0";
                    }
                    else
                    {
                        prepareRegexp += "[0-1]";
                    }
                }
            }

            Regex regex = new Regex(prepareRegexp);

            for (int i = 0; i < 10; i++)
            {
                if (regex.IsMatch(codesForConversion[i]))
                {
                    possibleMatches.Add(i);
                }
            }

            return possibleMatches;
        }
    }
}
