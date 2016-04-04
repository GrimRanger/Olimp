using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sergey_Alexeev
{
    public static class Helpers
    {
        static readonly bool[] _null = new bool[] { true, true, true, false, true, true, true };
        static readonly bool[] _one = new bool[] { false, false, true, false, false, true, false };
        static readonly bool[] _two = new bool[] { true, false, true, true, true, false, true };
        static readonly bool[] _three = new bool[] { true, false, true, true, false, true, true };
        static readonly bool[] _four = new bool[] { false, true, true, true, false, true, false };
        static readonly bool[] _five = new bool[] { true, true, false, true, false, true, true };
        static readonly bool[] _six = new bool[] { true, true, false, true, true, true, true };
        static readonly bool[] _seven = new bool[] { true, false, true, false, false, true, false };
        static readonly bool[] _eight = new bool[] { true, true, true, true, true, true, true };
        static readonly bool[] _nine = new bool[] { true, true, true, true, false, true, true };

        //Словарь с цифрами
        public static readonly Dictionary<int, bool[]> numeralsFromInt = new Dictionary<int, bool[]>
        {
            { 0, _null },
            { 1, _one },
            { 2, _two },
            { 3, _three },
            { 4, _four },
            { 5, _five },
            { 6, _six },
            { 7, _seven },
            { 8, _eight },
            { 9, _nine }
        };

        /// <summary>
        /// Перевод целого числа в кортеж из двух массивов с значениями ячеек
        /// </summary>
        /// <param name="value"></param>
        /// <param name="brokenCells">Значения битых ячеек. true - ячейка битая, false - иначе</param>
        /// <returns></returns>
        public static Tuple<bool[], bool[]> ToTuple(this int value, Tuple<bool [], bool[]> brokenCells)
        {
            //Получаем левую и правую цифры
            var leftNumeral = value / 10;
            var rightNumeral = value % 10;

            //Получаем соответствующие массивы с значениями ячеек
            var leftNumeralCells = numeralsFromInt[leftNumeral];
            var rightNumeralCells = numeralsFromInt[rightNumeral];

            //"гасим" битые ячейки
            leftNumeralCells = leftNumeralCells.Select((x, i) => brokenCells.Item1[i] ? false : x).ToArray();
            rightNumeralCells = rightNumeralCells.Select((x, i) => brokenCells.Item2[i] ? false : x).ToArray();

            return new Tuple<bool[], bool[]>(leftNumeralCells, rightNumeralCells);
        }
    }
}
