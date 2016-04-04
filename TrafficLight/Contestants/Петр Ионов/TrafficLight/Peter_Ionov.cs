using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peter_Ionov
{
    public class Peter_Ionov
    {
        const int UNKNOWN = 0; //будут помечаться ячейки, которые никогда не горели
        const  int UNBROKEN = 1; //будут помечаться ячейки, которые точно не сломаны (горели раньше)
        const int LIGHT = 2;    //будут помечаться горящие ячейки
        static  bool[,] numerals = new bool[,]
                              { 
                                {true, true, true, false, true, true, true},
                                {false, false, true, false, false, true, false},
                                {true, false, true, true, true, false, true},
                                {true, false, true, true, false, true, true},
                                {false, true, true, true, false, true, false},
                                {true, true, false, true, false, true, true},
                                {true, true, false, true, true, true, true},
                                {true, false, true, false, false, true, false},
                                {true, true, true, true, true, true, true},
                                {true, true, true, true, false, true, true}
                              };

        public static int isNum(int[] intFigure)
        {
            int result = -1;
            bool[] boolFigure = new bool[7];

            for (var i = 0; i < 7; i++)
                if (intFigure[i] == LIGHT)
                    boolFigure[i] = true;
                else
                    boolFigure[i] = false;

            for (var i = 0; i < 10; i++) 
            {
                bool isEquals = true;
                for (var j = 0; j < 7; j++)
                    if (boolFigure[j] != numerals[i, j])
                    {
                        isEquals = false;
                        break;
                    }
                if (isEquals)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public static bool BoolArrComparison(bool[] a, bool[] b)
        {
            bool res = true;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                {
                    res = false;
                    break;
                }
            return res;
        }

        public static SortedSet<int> PossibleNumerals(int[] trLightFig)
        {
            int i, fig;
            var result = new SortedSet<int>();

            if ((fig = isNum(trLightFig)) > -1)
                result.Add(fig);
            for (i = 0; i < 7; i++)
                if (trLightFig[i] == UNKNOWN)
                {
                    trLightFig[i] = LIGHT;
                    result.UnionWith(PossibleNumerals(trLightFig));
                    trLightFig[i] = UNKNOWN;
                }

            return result;
        }

        public static void SolveTrafficLightProblem(ITrafficLight trafficLight)
        {
            var leftCells = new int[7];
            var rightCells = new int[7];
            int leftNum = -1, prevLeftNum = -1, rightNum = -1, result = -1;
            var setRightNums = new SortedSet<int>();
            var setLeftNums = new SortedSet<int>();
            var setPrevLeftNums = new SortedSet<int>(); //здесь будут все возможные значения левой цифры, полученные на предыдущем шаге
            var setPrevRightNums = new SortedSet<int>(); //здесь будут все возможные значения правой цифры, полученные на предыдущем шаге
            var prevLeftFig = new bool[7];
            bool isFirstIteration = true;

            for (var i = 0; i < 7; i++)
            {
                leftCells[i] = UNKNOWN;
                rightCells[i] = UNKNOWN;
            }

            while (result < 0)
            {
                for (var i = 0; i < 7; i++) //выключение горящих ячеек
                {
                    if (leftCells[i] == LIGHT)
                        leftCells[i] = UNBROKEN;
                    if (rightCells[i] == LIGHT)
                        rightCells[i] = UNBROKEN;
                }

                if (!(trafficLight.GetNext()))
                {
                    result = 0;
                    break;
                }

                for (var i = 0; i < 7; i++)
                {
                    if (trafficLight.Current.Item1[i])
                        leftCells[i] = LIGHT;
                    if (trafficLight.Current.Item2[i])
                        rightCells[i] = LIGHT;
                }

                
                setLeftNums = PossibleNumerals(leftCells);
                if (!isFirstIteration && !BoolArrComparison(trafficLight.Current.Item1, prevLeftFig))
                {
                    var tempSet = new SortedSet<int>();
                    for (var i = 0; i < 10; i++)
                        if (setPrevLeftNums.Contains(i) && setLeftNums.Contains(i - 1))
                            tempSet.Add(i - 1);
                    setLeftNums = tempSet;
                }
                if (setLeftNums.Count == 1)
                    leftNum = setLeftNums.Min;
                  


                if (rightNum == -1)
                {
                    if (!isFirstIteration && !BoolArrComparison(trafficLight.Current.Item1, prevLeftFig))
                        rightNum = 9;
                    else
                    {
                        setRightNums = PossibleNumerals(rightCells);
                        if (leftNum > -1 && prevLeftNum > -1 && leftNum == prevLeftNum)
                        {
                            var tempSet = new SortedSet<int>();
                            for (var i = 0; i < 10; i++)
                                if (setPrevRightNums.Contains(i) && setRightNums.Contains(i - 1))
                                    tempSet.Add(i - 1);
                            setRightNums = tempSet;
                        }
                        if (setRightNums.Count == 1)
                            rightNum = setRightNums.Min;
                    }
                }
                else if (rightNum > 0)
                    rightNum--;
                else rightNum = 9;


                Array.Copy(trafficLight.Current.Item1, prevLeftFig, 7);
                if (leftNum > -1)
                    prevLeftNum = leftNum;
                setPrevLeftNums = setLeftNums;
                setPrevRightNums = setRightNums;
                isFirstIteration = false;

                if (rightNum > -1 && leftNum > -1)
                    result = leftNum * 10 + rightNum;

            }

            trafficLight.Answer(result);
        }

        static void Main(string[] args)
        {
        }
    }
}
