using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;


namespace Vasily_Golubtsov
{
    public class StateDetector
    {
        // Метод определит состояние светофора за минимальное количество вызовов GetNext()
        public void SolveTrafficLightProblem(ITrafficLight trafficLight)
        {
            int answer = 0;
            while (next(trafficLight))
            {
                case9();
                findBreaked(breakCellArrayLeft, curStateLeftArray);
                findBreaked(breakCellArrayRight, curStateRightArray);
                int newHypLeft = testHypothesis(breakCellArrayLeft, changeListLeft);
                int newHypRight = testHypothesis(breakCellArrayRight, changeListRight);
                if (curStateLeftDigit == -1)
                    curStateLeftDigit = newHypLeft;
                if (curStateRightDigit == -1)
                    curStateRightDigit = newHypRight;
                if (curStateLeftDigit != -1 && curStateRightDigit != -1)
                {
                    answer = curStateLeftDigit * 10 + curStateRightDigit;
                    break;
                }
            }
            trafficLight.Answer(answer);
                    
        }

        public StateDetector()
        {
            init();
        }

        int curStateLeftDigit;  // Текущее состояние левой цифры светофора
        int curStateRightDigit; // .. правой ..

        bool[] curStateLeftArray;    // хранит Current.Item1
        bool[] curStateRightArray;   // хранит Current.Item2

        // номер строки - цифра, 
        // строка матрици - её описание массивом в current без битых секторов
        bool[,] originalStatesMX; 
        
        // массив содержит информаию о предположительно битых ячеёках
        bool [] breakCellArrayLeft;
        bool [] breakCellArrayRight;

        // Список изменений цифр
        List<bool[]> changeListLeft;
        List<bool[]> changeListRight;

        int nextUseNumber; // сколько раз мы использовали функцию ITrafficLight::getNext()

        void init()
        {
            nextUseNumber = 0;
            curStateLeftDigit = -1; // неопределённость = -1
            curStateRightDigit = -1; // аналагично

            changeListLeft = new List<bool[]>();
            changeListRight = new List<bool[]>();

            curStateLeftArray = new bool[7];
            curStateRightArray = new bool[7];

            // по началу считаем все сломаными, при возникновении горящего считаем не сломаным
            breakCellArrayLeft = new bool[7] { true, true, true, true, true, true, true };
            breakCellArrayRight = new bool[7] { true, true, true, true, true, true, true};

            originalStatesMX = new bool [10, 7] {
                            {true, true, true, false, true, true, true},        // 0
                            {false, false, true, false, false, true, false},    // 1
                            {true, false, true, true, true, false, true},       // 2
                            {true, false, true, true, false, true, true},       // 3
                            {false, true, true, true, false, true, false},      // 4
                            {true, true, false, true, false, true, true},       // 5
                            {true, true, false, true, true, true, true},        // 6
                            {true, false, true, true, true, true, false},       // 7
                            {true, true, true, true, true, true, true},         // 8
                            {true, true, true, true, false, true, true}         // 9
            };

        }

        // считывает следующее значение светофора
        bool next(ITrafficLight trafficLight)
        {
            if (!trafficLight.GetNext())
                return false;
            nextUseNumber++;

            // + доп гипотиза о переключении левого
            nextCurStateDigit();
            fillCurState(trafficLight);
            return true;
        }

        // если левый переключатель переключился, с права точно девятка
        // вызывать после метода Next()
        void case9()
        {
            var lastInHistory = changeListLeft.Last();
            if (!equal(curStateLeftArray, lastInHistory))
                if (curStateRightDigit == -1)
                {
                    changeListLeft.Add(curStateLeftArray);
                    curStateRightDigit = 9;
                }
        }

        int testHypothesis(bool[] breakCellArray, List<bool[]> changeList)
        {
            // флагами отмечаем возможные цифры
            bool[] hypothesis = new bool[10];
            for (int i = 0; i<10; i++)
                hypothesis[i] = false;

            for (int diggit = 0; diggit < 10; diggit++)
                if (testHypothesis(diggit, changeList, breakCellArray))
                    hypothesis[diggit] = true;
            int hypState = numberTrueHypothesis(hypothesis);
            return hypState;
        }



        // возвращает единственно правдивую гипотизу или -1 если таковой нет
        int numberTrueHypothesis(bool[] hypothesis)
        {
            int trueHyp = -1;
            for (int i = 0; i < hypothesis.Length; i++)
                if (hypothesis[i] == true)
                    if (trueHyp == -1)
                        trueHyp = i;
                    else
                        return -1;
            return trueHyp;
        }

        // проверяет может ли быть в данный моммент цифра diggit
        bool testHypothesis(int diggit, List<bool[]> history, bool[] breakArray)
        {
            int histSize =  history.Count();
            var oriMXList = formSubMxForHypothesis(diggit, histSize);
            for (int i = 0; i < histSize; i++)
            {
                var histRow = history[histSize - i - 1];
                var oriRow = oriMXList[i];
                if (!equal(histRow, oriRow, breakArray))
                    return false;
            }
            return true;
        }

        // проверяет может ли быть равна строка своему оригиналу если считать что определёные в breakCellArray ячейки битые 
        bool equal(bool[] row, bool[] originalRow, bool[] breakCellArray)
        {
            for (int i=0; i<row.Length; i++)
            {
                bool curBit = row[i];
                bool oriBit = originalRow[i];
                bool isBreak = breakCellArray[i];
                if (!isBreak)
                    if (curBit != oriBit)
                        return false;
            }
            return true;
        }

        bool equal(bool[] row, bool[] originalRow)
        {
            for (int i = 0; i < row.Length; i++)
                if (row[i] != originalRow[i])
                    return false;
            return true;
        }

        // формирует субматрицу из строк оригинала в обратном порядке 
        // для параметров 1, 3 из оригинальной матрицы будут взяты 1, 0 и 9 строки
        List<bool[]> formSubMxForHypothesis(int digit, int size)
        {
            List<bool[]> subMX = new List<bool[]>();

            int posInOriginalMx = digit;
            for (int i = 0; i < size; i++)
            {
                if (posInOriginalMx > 9)
                    posInOriginalMx = 0;
                bool[] originalState = originalStatesMX.GetRow(posInOriginalMx); 
                subMX.Add(originalState);
                posInOriginalMx++;
            }
            return subMX;
        }

        // поправляет массивы breakCellArrayLeft и breakCellArrayRight 
        void findBreaked(bool[] oldBreakeArray, bool[] newData)
        {
            for (int i = 0; i < 7; i++)
                if (newData[i] == true && oldBreakeArray[i] == true)
                    oldBreakeArray[i] = false;
        }

        void fillCurState(ITrafficLight trafficLight)
        {
            curStateLeftArray = new bool[7];
            curStateRightArray = new bool[7];

            curStateLeftArray.Set(trafficLight.Current.Item1);
            curStateRightArray.Set(trafficLight.Current.Item2);
         
            if (changeListRight.Count < 10) // нет смысла записывать историю более чем 10 изменений правого сектора
                changeListRight.Add(curStateRightArray);

            if (changeListLeft.Count < 10)
                if (curStateRightDigit == 9 || 
                   curStateRightDigit == -1 && nextUseNumber % 10 == 0 ||
                    changeListLeft.Count == 0)
                {
                    if (curStateRightDigit == -1 && changeListLeft.Count != 0)
                        curStateLeftDigit--;
                    changeListLeft.Add(curStateLeftArray);
                }
        }

        void nextCurStateDigit()
        {
            if (curStateRightDigit != -1)
            {
                curStateRightDigit--;
                if (curStateRightDigit < 0)
                {
                    curStateRightDigit = 9;
                    if (curStateLeftDigit != -1)
                        curStateLeftDigit--;
                }
            }

        }

    }
}
