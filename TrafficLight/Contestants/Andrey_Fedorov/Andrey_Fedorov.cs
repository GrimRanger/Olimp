using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Interfaces;

namespace Andrey_Fedorov
{
    public  class Andrey_Fedorov
    {
        public void SolveTrafficLightProblem(ITrafficLight trafficLight)
        {
            bool[][] numerals =
            {
                new[] {true, true, true, false, true, true, true}, // 0
                new[] {false, false, true, false, false, true, false}, // 1
                new[] {true, false, true, true, true, false, true}, // 2
                new[] {true, false, true, true, false, true, true}, // 3
                new[] {false, true, true, true, false, true, false}, // 4
                new[] {true, true, false, true, false, true, true}, // 5
                new[] {true, true, false, true, true, true, true}, // 6
                new[] {true, false, true, false, false, true, false}, // 7
                new[] {true, true, true, true, true, true, true}, // 8
                new[] {true, true, true, true, false, true, true} // 9
            };

            var vList1 = new List<int>();
            var vList2 = new List<int>();
            var vList = new List<int>();
            var tmpList = new List<int>();

            int step = 0, result = 0;
            bool fit1 = true, fit2 = true, myFlag = true;

            trafficLight.GetNext();

            while (myFlag)
            {
                for(int j = 0; j < 10; j++)
                {
                    for (var i = 0; i < 7; i++)
                    {
                        if (trafficLight.Current.Item1[i] && numerals[j][i] == false)
                            fit1 = false;
                        if (trafficLight.Current.Item2[i] && numerals[j][i] == false)
                            fit2 = false;
                    }

                    if (fit1) vList1.Add(j);
                    else fit1 = true;

                    if (fit2) vList2.Add(j);
                    else fit2 = true;
                }

                foreach (int num1 in vList1)
                    foreach (int num2 in vList2)
                    {
                        int num = 10 * num1 + num2;
                        if (step == 0)
                            vList.Add(num);
                        else
                        {
                            for (int i = 0; i < vList.Count; i++)
                            {
                                if (vList[i] == num + step)
                                    if (!tmpList.Contains(vList[i]))
                                        tmpList.Add(vList[i]);
                            }
                        }

                    }

                vList1.Clear();
                vList2.Clear();
                if (step != 0)
                    vList.Clear();

                for (int i = 0; i < tmpList.Count; i++)
                    vList.Add(tmpList[i]);

                tmpList.Clear();

                if (vList.Count == 1)
                {
                    result = vList[0];
                    myFlag = false;
                }
                else
                {
                    step++;
                    if (!trafficLight.GetNext())
                    {
                        result = step;
                        myFlag = false;
                    }

                }
            }

            trafficLight.Answer(result);
        }

    }
}