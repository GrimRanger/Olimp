using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarachevProject
{
    public class TrafficLightHelper
    {
        private static bool[][] Ideals;//Образа цифр в булевском массиве
        private const int DigitCellsCount = 7;
        private const int DigitsCount = 10;

        private bool[] _firstDigitCellsStatus;//Статусы ячеек у светофора(true - работает, false - не определено)
        private bool[] _secondDigitCellsStatus;
        private List<int> _сandidates;//Числа-кандидаты на ответ
        static TrafficLightHelper()
        {
            Ideals = new bool[DigitsCount][];
            Ideals[0] = new bool[] { true, true, true, false, true, true, true };//012456
            Ideals[1] = new bool[] { false, false, true, false, false, true, false };//25
            Ideals[2] = new bool[] { true, false, true, true, true, false, true };//02346
            Ideals[3] = new bool[] { true, false, true, true, false, true, true };//02356
            Ideals[4] = new bool[] { false, true, true, true, false, true, false };//1235
            Ideals[5] = new bool[] { true, true, false, true, false, true, true };//01356
            Ideals[6] = new bool[] { true, true, false, true, true, true, true };//013456
            Ideals[7] = new bool[] { true, false, true, false, false, true, false };//025
            Ideals[8] = new bool[] { true, true, true, true, true, true, true };//0123456
            Ideals[9] = new bool[] { true, true, true, true, false, true, true };//012356
        }

        public TrafficLightHelper()
        {
            _firstDigitCellsStatus = new bool[DigitCellsCount];
            _secondDigitCellsStatus = new bool[DigitCellsCount];
            _сandidates = new List<int>();
            for (int i = 0; i < DigitsCount * DigitsCount; i++)
            {
                _сandidates.Add(i + 1);
            }
        }

        public bool UpdateCandidates(Tuple<bool[], bool[]> digitsTuple, out int number)
        {
            for (int i = 0; i < _сandidates.Count; i++)
            {
                _сandidates[i]--;
            }
            _сandidates.Remove(0);
            for (int i = 0; i < DigitCellsCount; i++)
            {
                if (digitsTuple.Item1[i]) _firstDigitCellsStatus[i] = true;
                if (digitsTuple.Item2[i]) _secondDigitCellsStatus[i] = true;
            }
            int[] firstDigitCandidates;
            int[] secondDigitCandidates;
            SplitCandidates(out firstDigitCandidates, out secondDigitCandidates);
            for (int i = 0; i < secondDigitCandidates.Length; i++)
            {
                if (!StillCandidateDigit(secondDigitCandidates[i], digitsTuple.Item2, _secondDigitCellsStatus))
                {
                    _сandidates.RemoveAll(a => a % 10 == secondDigitCandidates[i]);
                }
            }
            for (int i = 0; i < firstDigitCandidates.Length; i++)
            {
                if (!StillCandidateDigit(firstDigitCandidates[i], digitsTuple.Item1, _firstDigitCellsStatus))
                {
                    _сandidates.RemoveAll(a => a / 10 == firstDigitCandidates[i]);
                }
            }
            number = 0;
            if (_сandidates.Count == 1)
            {
                number = _сandidates[0];
                return true;
            }
            return false;
        }
        /*
        на выход 2 массива: 
        в первом - всевозможные 1-ые цифры у оставшихся кандидатов
        во втором - вторые 
        */
        private void SplitCandidates(out int[] firstDigits, out int[] secondDigits)
        {
            var firstDigitCandidates = new HashSet<int>();
            var secondDigitCandidates = new HashSet<int>();
            for (int i = 0; i < _сandidates.Count; i++)
            {
                firstDigitCandidates.Add(_сandidates[i] / 10);
                secondDigitCandidates.Add(_сandidates[i] % 10);
            }
            firstDigits = firstDigitCandidates.ToArray();
            secondDigits = secondDigitCandidates.ToArray();
        }
        //указывает на то, является ли входная цифра возможной сейчас в заданной ячейке 
        private bool StillCandidateDigit(int digit, bool[] digitCells, bool[] status)
        {
            var cells = Ideals[digit];
            for (int i = 0; i < DigitCellsCount; i++)
            {
                if (status[i] && (digitCells[i] != cells[i])) return false;
            }
            return true;
        }
    }
}
