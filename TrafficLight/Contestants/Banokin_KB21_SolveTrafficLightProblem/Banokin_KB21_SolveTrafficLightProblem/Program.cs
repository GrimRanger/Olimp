using System;
using System.Collections.Generic;

/*
Confirmit Олимпиада C# 2016
TrafficLightProblem

Банокин Дмитрий Александрович
ЯрГУ, математический факультет, КБ-21-СО
dirdash@gmail.com
+79159886970

Моё решение данного задания основано на анализе светящихся ячеек на табло и запоминании их в качестве однозначно работающих,
это осуществляется методом CheckWorkingCells, а данные о работающих ячейках хранятся в Tuple<bool[], bool[]> workingCells.
Далее с помощью метода DetermineDigit идёт сравнение того, что горит на табло, со всеми возможными представлениями цифр от 0 до 9
в виде массива bool. Сравниваются комбинации ячеек, которые однозначно горят ("видно" через trafficLight.Current) и однозначно НЕ горят
(определяются с помощью сравнения trafficLight.Current и workingCells). Если метод DetermineDigit может однозачно указать, что полученная им
комбинация удовлетворяет только одной единственной цифре, то он вернёт её, и значение для одной цифры светофора будет определено.
Метод SolveTrafficLightProblem сначала обрабатывает правую цифру на светофоре (вызывая метод GetNext, пока не сможет её определить, или
определяя её 9-кой при изменении левой цифры). Когда правая цифра определена, аналогичной обработке подвергается - правая (GetNext()
вызывается столько раз, сколько необходимо для изменения левой цифры и получения возможности проводить анализ на основе этого изменения).
Если SolveTrafficLightProblem не может однозначно точно определить число и тратит все попытки вызова GetNext, он отправляет 0 в качестве ответа.

 *Для workingCells, SolveTrafficLightProblem, CheckWorkingCells, DetermineDigit прошу при необходимости убрать static.
*/

namespace Banokin_KB21_SolveTrafficLightProblem
{
    public interface ITrafficlight
    {
        bool GetNext();
        Tuple<bool[], bool[]> Current { get; set; }
        void Answer(int value);
    }

    class Program
    {
        static void Main(string[] args)
        {

        }

        // Словарь для представления цифр в виде массива bool
        public static readonly Dictionary<int, bool[]> digitToBullArray = new Dictionary<int, bool[]>
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
        // Информация о номерах однозначно работающих ячеек
        static Tuple<bool[], bool[]> workingCells = new Tuple<bool[], bool[]>(new bool[7], new bool[7]);

        public static void SolveTrafficLightProblem(ITrafficlight trafficLight)
        {
            int leftDigit = 0;
            int rightDigit = 0;
            bool leftDigitIsDetermined = false;
            bool rightDigitIsDetermined = false;
            bool currentIsNotZero = true;
            Tuple<bool[], bool[]> previous = new Tuple<bool[], bool[]>(new bool[7], new bool[7]); // Состояние светофора на предыдущем шаге

            for (int i = 0; i < 7; i++) // false соответсвует тому, что нельзя однозначно сказать, работает ли ячейка
            {
                workingCells.Item1[i] = false;
                workingCells.Item2[i] = false;
            }

            currentIsNotZero = trafficLight.GetNext(); // "Нулевой" шаг для получения значения в trafficLight.Current
            if (currentIsNotZero == false) // Светофор показывает 00
            {
                leftDigitIsDetermined = true;
                rightDigitIsDetermined = true;
            }

            while (!leftDigitIsDetermined || !rightDigitIsDetermined) // Основой цикл программы, на каждом шаге идёт попытка обработотать цифры на светофоре,
            {                                                         // пока информация о них не будет получена, или светофор не закончит отсчёт
                if (!rightDigitIsDetermined) // Обработка правой цифры
                {
                    CheckWorkingCells(trafficLight.Current); // Обновление информации о работающих ячейках
                    int digitOnRightPanel = DetermineDigit(trafficLight.Current.Item2, workingCells.Item2); // Попытка определить правую цифру
                    if (digitOnRightPanel != -1) // Правая цифра определена
                    {
                        rightDigit = digitOnRightPanel;
                        rightDigitIsDetermined = true;
                    }
                    else // Правая цифра не определена, нужно выполнить GetNext()
                    {
                        previous = trafficLight.Current;
                        currentIsNotZero = trafficLight.GetNext();
                        if (currentIsNotZero == false) // Число попыток израсходовано, светофор показывает 00
                        {
                            leftDigit = 0;
                            rightDigit = 0;
                            leftDigitIsDetermined = true;
                            rightDigitIsDetermined = true;
                            break;
                        }
                        if (previous.Item1 != trafficLight.Current.Item1) // Если левая цифра изменилась, значит, мы "перешагнули" десяток, и правая цифра - это 9 
                        {
                            rightDigit = 9;
                            rightDigitIsDetermined = true;
                        }
                    }
                }
                if (rightDigitIsDetermined) //Правая цифра определена, обработка левой
                {
                    CheckWorkingCells(trafficLight.Current); // Обновление информации о работающих ячейках
                    int digitOnLeftPanel = DetermineDigit(trafficLight.Current.Item1, workingCells.Item1); // Попытка определить левую цифру
                    if (digitOnLeftPanel != -1) // Левая цифра определена
                    {
                        leftDigit = digitOnLeftPanel;
                        leftDigitIsDetermined = true;
                    }
                    else // Левая цифра не определена, нужно выполнить GetNext()
                    {
                        if (rightDigit != 9) // Для обновления левой цифры достаточно вызова GetNext "правая цифра" + 1 раз
                        {
                            for (int i = 0; i < rightDigit + 1; i++)
                            {                                
                                currentIsNotZero = trafficLight.GetNext();
                                if (currentIsNotZero == false) // Число попыток израсходовано, светофор показывает 00
                                {
                                    leftDigitIsDetermined = true;
                                    leftDigit = 0;
                                    rightDigit = 0;
                                    break;
                                }
                            }
                            if (leftDigitIsDetermined != true) // Если левая цифра не определилась 0-ём при последнем возможном GetNext
                                rightDigit = 9;
                        }
                        else
                            for (int i = 0; i < 10; i++) // Правая цифра - это 9, и необходимо 10 раз вызвать GetNext, чтобы обновить левую цифру
                            {
                                currentIsNotZero = trafficLight.GetNext();
                                if (currentIsNotZero == false) // Число попыток израсходовано, светофор показывает 00
                                {
                                    leftDigitIsDetermined = true;
                                    leftDigit = 0;
                                    rightDigit = 0;
                                    break;
                                }
                            }
                    }
                }
            }
            // Формирование и отправка ответа
            int result = leftDigit * 10 + rightDigit; 
            trafficLight.Answer(result);
        }

        // Выполняет обновление информации об однозачно работающих ячейках, "рассматривая" работающие на trafficLight.Current
        public static void CheckWorkingCells(Tuple<bool[], bool[]> Current)
        {
            for (int i = 0; i < 7; i++)
            {
                if (Current.Item1[i] == true)
                    workingCells.Item1[i] = true;
                if (Current.Item2[i] == true)
                    workingCells.Item2[i] = true; ;
            }
        }

        // Пытается определить цифру на посланном табло (digitOnPanel), сравнивая его со словарём digitToBullArray, учитывая работоспособность ячеек (workingCellsOnPanel)
        // Возвращает определённую цифру в случае  успеха, и - -1 в случае неудачи
        public static int DetermineDigit(bool[] digitOnPanel, bool[] workingCellsOnPanel)
        {
            int result = -1;
            string[] cellsOnPanel = new string[7] { "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown" }; // Определяет комбинацию горящих (Lights) и негорящих (Not lights) ячеек
                                                                                                                                   // По умолчанию, информации нет (Unknown)
            for (int i = 0; i < 7; i++) // Заполнение информации об однозначно горящих (Lights) и однозначно негорящих ячейках (Not lights)
            {
                if (digitOnPanel[i] == true)
                    cellsOnPanel[i] = "Lights";
                if (workingCellsOnPanel[i] == true && digitOnPanel[i] == false)
                    cellsOnPanel[i] = "Not lights";
            }
            int numberOfMatches = 0; // Количество сопадений поданной в метод цифры и цифры из словаря digitToBullArray
            for (int i = 0; i < 10; i++) // Сравнение с цифрами от 0 до 9
            {
                bool[] digitAsBoolArray = digitToBullArray[i]; 
                bool digitsAreMatch = true; // По умолчанию считаем, что поданная цифра и цифра из словаря совпадают
                for (int j = 0; j < 7; j++)
                {
                    // Ищем несовпадение: если ячейка по факту горит, но для заданной цифры из словаря - гореть не должа, то это несовпадение
                    //                    если ячейка по факту не горит, но для заданной цифры из словаря - гореть должа, то это несовпадение
                    if ((cellsOnPanel[j] == "Lights" && digitAsBoolArray[j] == false) || (cellsOnPanel[j] == "Not lights" && digitAsBoolArray[j] == true))
                    {
                        digitsAreMatch = false;
                        break;
                    }
                }
                if (digitsAreMatch) 
                {
                    numberOfMatches++;
                    if (numberOfMatches > 1) // Если наблюдается более одного совпадения, то однозачно определить цифру невозможно - возвращается -1
                        return -1;
                    else
                        result = i;
                }
            }

            return result;
        }
    }
}
