using System;
using System.Collections.Generic;

namespace IlyashenkoKB21
{
    class Program
    {
        //Решение задачи TrafficlightProblem
        //Ильяшенко Павел, КБ21СО, ЯрГУ им. П.Г.Демидова, Математический факультет
        static void Main(string[] args)
        {
            //В метод SolveTrafficLightProblem передаем экземпляр интерфейса ITrafficLight
            SolveTrafficLightProblem(trafficlight);
        }
        //GoodBadUnknown хранит информацию о состоянии ячеек: рабочие ли они, битые или же об их состоянии ничего не известно. GoodBadUnknown будет заполняться по мере вызовов метода GetNext.
        public static Tuple<string[], string[]> GoodBadUnknown = new Tuple<string[], string[]>(new string[7], new string[7]);
        //Словарь для преобразования значений типа int в массив bool[]
        #region Dictionary
        public static readonly Dictionary<int, bool[]> getDigit = new Dictionary<int, bool[]>
        {
            {0, new bool[] { true, true, true, false, true, true, true}},
            {1, new bool[] { false, false, true, false, false, true, false}},
            {2, new bool[] { true, false, true, true, true, false, true}},
            {3, new bool[] { true, false, true, true, false, true, true }},
            {4, new bool[] { false, true, true, true, false, true, false}},
            {5, new bool[] { true, true, false, true, false, true, true }},
            {6, new bool[] { true, true, false, true, true, true, true }},
            {7, new bool[] { true, false, true, false, false, true, false }},
            {8, new bool[] { true, true, true, true, true, true, true }},
            {9, new bool[] { true, true, true, true, false, true, true }}
        };
        #endregion
        //Метод UpdateCellStats будет помечать ячейки в GoodBadUnknown, если они будут загораться на табло светофора.
        public static void UpdateCellStats(Tuple<bool[], bool[]> Current)
        {
            for (int i = 0; i < 7; i++)
            {
                if (Current.Item1[i] == true)
                    GoodBadUnknown.Item1[i] = "Good";
                if (Current.Item2[i] == true)
                    GoodBadUnknown.Item2[i] = "Good";
            }
        }
        //Метод Determinator определяет текущую цифру на табло светофора и возвращяет либо эту цифру, либо -1, если невозможно определить цифру однозначно.
        public static int Determinator(bool[] input, string[] workingCells) //В этот метод передается массив типа bool[] для текщей цифры и информация о состоянии ячеек светофора.
        {
            int result = -1;
            string[] toCompare = new string[7] { "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown" }; //Массив типа string[] для пометки ячеек как Горящие/Не горящие
            for (int i = 0; i < 7; i++) //Помечаем элементы массива toCompare. 
            {
                if (workingCells[i] == "Good" && input[i] == false) //Если ячейка рабочая, но не горит, помечаем как Does not light
                    toCompare[i] = "Does not light";
                if (input[i] == true) //Если ячейка горит, помечаем как Lights
                    toCompare[i] = "Lights";
            }
            int numberOfMatches = 0; //Переменная для подсчета количества совпадений текущего состояния цифры на светофоре с правильным отображением цифр
            for (int i = 0; i < 10; i++) //Сверяем текущее состояние цифры с десятью правильными отображениями цифр.
            {
                bool[] iInBool = getDigit[i]; //Получаем представление цифры в виде массива bool[]
                bool Matches = true; //Переменная для пометки совпадения цифр
                for (int j = 0; j < 7; j++) //Последовательно сверяем ячейки на табло светофора
                {
                    //Если ячейка горит, но не должна, или не горит, а должна, значит цифры не совпадают.
                    if ((toCompare[j] == "Lights" && iInBool[j] == false) || (toCompare[j] == "Does not light" && iInBool[j] == true))
                    {
                        Matches = false;
                        break;
                    }
                }
                if (Matches)
                {
                    numberOfMatches++; //Если получено совпадение цифр, увеличиваем число совпадений
                    result = i;
                }
            }
            if (numberOfMatches != 1) //Если число совпадений больше одного, мы не можем однозначно определить текущую цифру на табло. Возвращаем -1.
                return -1;
            else
                return result;
        }
        //Основной метод программы. Выполняет обработку экземпляра интерфейса и на выходе вызывает метод Answer.
        public static void SolveTrafficLightProblem(ITrafficlight trafficLight)
        {
            int first = 0, second = 0; //Переменные для первой и второй цифр на табло светофора.
            bool firstIsDone = false, secondIsDone = false; //Переменные, хранящие значение true, если значения первой и второй цифр на табло определены.
            Tuple<bool[], bool[]> Previous = null; //Хранит предыдущее значение табло светофора
            
            bool IsNotNull = trafficLight.GetNext(); //Первый вызов метода GetNext
            if (IsNotNull == false) //Если отсчет закончен и на табло 00, помечаем первую и вторую цифры как определенные.
            {
                firstIsDone = true;
                secondIsDone = true;
            }
            for (int i = 0; i < 7; i++) //Помечаем состояние ячеек светофора как неопределенное - Unknown
            {
                GoodBadUnknown.Item1[i] = "Unknown";
                GoodBadUnknown.Item2[i] = "Unknown";
            }
            //Цикл будет выполняться, пока не будут определены первая и вторая цифры на табло светофора
            while (!firstIsDone || !secondIsDone)
            {
                if (!secondIsDone) //Определяем правую цифру на табло светофора
                {
                    UpdateCellStats(trafficLight.Current); //Обновление состояния ячеек светофора
                    int currDigit = Determinator(trafficLight.Current.Item2, GoodBadUnknown.Item2); //Определение текущей цифры с помощью метода Determinator
                    if (currDigit != -1) //Если цифра определена, записываем ее в переменную second и помечаем как определенную
                    {
                        second = currDigit;
                        secondIsDone = true;
                    }
                    if (!secondIsDone) //Если цифра еще не была определена, записываем текущее значение табло в переменную Previous и вновь вызываем метод GetNext.
                    {
                        Previous = trafficLight.Current;
                        bool NotZero = trafficLight.GetNext();
                        if (!NotZero)//Если GetNext вернул false, значит отсчет закончен и на табло горит 00. Помечаем первую и вторую цифры как определенные. 
                        {
                            first = 0;
                            second = 0;
                            firstIsDone = true;
                            secondIsDone = true;
                            break;
                        }
                        //Если в процессе обработки второй цифры произошло изменение первой, мы можем однозначно сказать, 
                        //что произошла смена разряда десятков, значит, значение правой цифры равно 9. Отсюда мы можем сразу перейти к обработке левой цифры.
                        if (Previous.Item1 != trafficLight.Current.Item1) 
                        {
                            second = 9;
                            secondIsDone = true;
                        }
                    }
                    
                }
                else //Заходим в этот блок кода только если правая цифра на табло уже определена.
                {
                    UpdateCellStats(trafficLight.Current); //Обновление состояния ячеек светофора
                    int currDigit = Determinator(trafficLight.Current.Item1, GoodBadUnknown.Item1); //Определение текущей цифры с помощью метода Determinator
                    if (currDigit != -1) //Если цифра определена, записываем ее в переменную first и помечаем как определенную
                    {
                        first = currDigit;
                        firstIsDone = true;
                        break;
                    }
                    bool NotZero = true;
                    for (int i = 0; i < 10; i++) //Для изменения первой цифры нам необходимо вызывать метод GetNext по 10 раз.
                    {
                        NotZero = trafficLight.GetNext();
                        UpdateCellStats(trafficLight.Current); //Обновление состояния ячеек светофора
                        if (!NotZero) //Если GetNext вернул false, значит отсчет закончен и на табло горит 00. Помечаем первую цифру как определенную.
                        {
                            first = 0;
                            second = 0;
                            firstIsDone = true;
                            break;
                        }
                    }
                }
            }
            trafficLight.Answer((first*10)+second); //Передаем в метод Answer ответ - текущее число на табло светофора.
        }
        
    }
    //Интерфейс ITrafficLight
    public interface ITrafficlight
    {
        bool GetNext();
        Tuple<bool[], bool[]> Current { get; }
        void Answer(int value);
    }
}