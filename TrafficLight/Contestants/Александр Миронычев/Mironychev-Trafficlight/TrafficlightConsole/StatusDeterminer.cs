using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Interfaces;

namespace Alexander_Mironych
{
    /// <summary>
    /// Класс, определяющий состояние светофора за минимальное число шагов
    /// </summary>
    public class StatusDeterminer
    {
        #region Display symbol references
        /// <summary>
        /// Ассоциативный массив состояний дисплея и значений на экране
        /// </summary>
        public static readonly Dictionary<int, SymbolState>
            references = new Dictionary<int, SymbolState>()
            {
                { 0, new SymbolState(new [] { true, true, true, false, true, true, true })},
                { 1, new SymbolState(new [] { false, false, true, false, false, true, false })},
                { 2, new SymbolState(new [] { true, false, true, true, true, false, true })},
                { 3, new SymbolState(new [] { true, false, true, true, false, true, true })},
                { 4, new SymbolState(new [] { false, true, true, true, false, true, false })},
                { 5, new SymbolState(new [] { true, true, false, true, false, true, true })},
                { 6, new SymbolState(new [] { true, true, false, true, true, true, true })},
                { 7, new SymbolState(new [] { true, false, true, false, false, true, false })},
                { 8, new SymbolState(new [] { true, true, true, true, true, true, true })},
                { 9, new SymbolState(new [] { true, true, true, true, false, true, true })}
            };
        #endregion


        #region Public methods
        /// <summary>
        /// Вычислить состояние светофора за минимальное число шагов
        /// </summary>
        public static void Determine(ITrafficLight trafficlight)
        {
            /* Инициализация */
            trafficlight.GetNext(); 

            /* Вычисляем возможные состояния дисплея */
            List<int> answers = PossibleTrafficlightState(trafficlight);

            while (answers.Count > 1)
            {
                /* Проверяем, не закончился ли отсчет */
                if (!trafficlight.GetNext())
                {
                    trafficlight.Answer(0);
                    return;
                }

                /* Вычисляем ожидаемые значения на дисплее */
                List<int> expected = GetExpected(answers);

                /* Вычисляем возможные состояния дисплея */ 
                answers = PossibleTrafficlightState(trafficlight);

                /* Находим совпадение значений из двух списков */
                answers = FilterNumbers(answers, expected);
            }

            /* В случае, если светофор неисправен, генерируем исключение */
            if (answers.Count == 0) 
                throw new Exception("Status could not be determined!");

            trafficlight.Answer(answers[0]);
        }
        #endregion


        #region Utils
        /// <summary>
        /// Вычисляет возможные цифры на дисплее
        /// </summary>
        private static List<int> PossibleDigits(bool[] state)
        {
            List<int> possible = new List<int>();

            foreach (KeyValuePair<int, SymbolState> pair in references)
                if (pair.Value.ContainsState(state))
                    possible.Add(pair.Key);

            return possible;
        }

        /// <summary>
        /// Вычисляет возможные состояния светофора 
        /// </summary>
        private static List<int> PossibleTrafficlightState(ITrafficLight trafficlight)
        {
            /* Вычисляем возможные цифры на дисплее */
            List<int> possibleFirst = PossibleDigits(trafficlight.Current.Item1);
            List<int> possibleSecond = PossibleDigits(trafficlight.Current.Item2);

            /* Вычисляем декартово произведение первого и второго множества */
            List<int> possible = new List<int>();
            foreach (int first in possibleFirst)
                foreach (int second in possibleSecond)
                    possible.Add(first * 10 + second);

            return possible;
        }

        /// <summary>
        /// Вычисляет ожидаемые числа на дисплее
        /// </summary>
        private static List<int> GetExpected(List<int> numbers)
        {
            List<int> expected = new List<int>();
            foreach (int number in numbers)
                if (number > 1)
                    expected.Add(number - 1);

            return expected;
        }

        /// <summary>
        /// Отсеиваеть не ожидаемые числа
        /// </summary>
        private static List<int> FilterNumbers(List<int> numbers, List<int> expected)
        {
            List<int> result = new List<int>();

            foreach (int number in numbers)
                if (expected.Contains(number))
                    result.Add(number);

            return result;
        }
        #endregion


        #region Demonstration
        /// <summary>
        /// Пошаговая демонстрация процесса вычисления состояния
        /// </summary>
        public static IEnumerator<List<int>> Demo(ITrafficLight trafficlight)
        {
            trafficlight.GetNext();
            List<int> answers = PossibleTrafficlightState(trafficlight);

            yield return answers;

            while (answers.Count > 1)
            {
                if (!trafficlight.GetNext())
                {
                    trafficlight.Answer(0);
                    yield break;
                }

                List<int> expected = GetExpected(answers);

                answers = PossibleTrafficlightState(trafficlight);
                answers = FilterNumbers(answers, expected);

                yield return answers;
            }

            if (answers.Count == 0)
                throw new Exception("Status could not be determined!");

            trafficlight.Answer(answers[0]);

            yield return answers;
        }
        #endregion
    }
}
