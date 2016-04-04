using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace Traffic2
{
    public class Anna_Galaida
    {
          public static void SolveTrafficLightProblem(ITrafficLight trafficLight)
        {
            trafficLight.GetNext();
            var curr = trafficLight.Current;
            bool[] bools1 = new bool[7], bools2 = new bool[7];
            curr.Item1.CopyTo(bools1, 0);
            curr.Item1.CopyTo(bools2, 0);
            var prev = new Tuple<bool[], bool[]>(bools1, bools2);
            int[][] changed = new int[][]
            {
                new int[] {0, 0, 0, 0, 0, 0, 0},
                new int[] {0, 0, 0, 0, 0, 0, 0}
            };
            bool[][] vars = new bool[][] 
            {
                new bool[] { false, false, false, false, false, false, false, false, false, false },
                new bool[]{ false, false, false, false, false, false, false, false, false, false}
            }; 
            bool start = true;
            int t = 0;
            int resultFirst = 0;
            int resultSecond = 0;
            int first = IsAll(vars[0]);
            int second = IsAll(vars[1]);
            int result = 0;

            while ((first < 0 || second < 0) && trafficLight.GetNext())
            {
                curr = trafficLight.Current;
                for (int i = 0; i < 7; i++)
                {
                    if (curr.Item1[i] != prev.Item1[i])
                    {
                        if (curr.Item1[i])
                            changed[0][i] = 1;
                        if (!curr.Item1[i])
                            changed[0][i] = -1;
                    }
                    if (curr.Item2[i] != prev.Item2[i])
                    {
                        if (curr.Item2[i])
                            changed[1][i] = 1;
                        if (!curr.Item2[i])
                            changed[1][i] = -1;
                    }
                }
                vars[0] = FindDigit(changed[0], vars[0], start);
                vars[1] = FindDigit(changed[1], vars[1], start);
                start = false;
                changed = new int[][]
                {
                    new int[] {0, 0, 0, 0, 0, 0, 0},
                    new int[] {0, 0, 0, 0, 0, 0, 0}
                };
                t++;
                first = IsAll(vars[0]);
                second = IsAll(vars[1]);
                if (first > 0)
                    resultFirst = first;
                if (second > 0)
                    resultSecond = second;
                curr.Item1.CopyTo(bools1, 0);
                curr.Item1.CopyTo(bools2, 0);
                prev = new Tuple<bool[], bool[]>(bools1, bools2);
            }
            result = t*10 + t + resultFirst*10 + resultSecond;
            trafficLight.Answer(result);
        }

        public static int IsAll(bool[] temp)
        {
            int count = 0;
            int k = 0;
            for (int i = 0; i < 10; i++)
            {
                if (temp[i])
                {
                    k = i;
                    count++;
                }
            }
            if (count == 1)
            {
                return k;
            }
            return -1;
        }

        public static bool[] FindDigit(int[] changed, bool[] prev, bool startFlag)
        {
            int[][] nums = new int[][]
            {
                new int[] {1, 1, 0, 0, 1, 0, 1},
                new int[] {-1, 0, 0, -1, -1, 1, -1},
                new int[] {0, 0, 0, 0, 1, -1, 0},
                new int[] {1, -1, 0, 0, 0, 0, 1},
                new int[] {-1, 0, 1, 0, 0, 0, -1},
                new int[] {0, 0, 0, 0, -1, 0, 0},
                new int[] {0, 1, -1, 1, 1, 0, 1},
                new int[] {0, -1, 0, -1, -1, 0, -1},
                new int[] {0, 0, 0, 0, 1, 0, 0},
                new int[] {0, 0, 0, 1, -1, 0, 0}
            };
            bool[] curr = new bool[] {false, false, false, false, false, false, false, false, false, false};
            bool[] result = new bool[] { false, false, false, false, false, false, false, false, false, false };
            bool flag = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (changed[j] != nums[i][j] && changed[j] != 0)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    curr[i] = true;
                flag = false;
            }
            if (!startFlag)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (curr[i])
                        if (prev[i + 1])
                            result[i] = true;
                }
                if(curr[9])
                    if (prev[0])
                        result[9] = true;
                return result;
            }
            else
            {
                return curr;
            }
        }
    }
    }
