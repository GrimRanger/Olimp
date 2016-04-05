using System;
using System.Collections.Generic;
using System.Linq;
using TrafficLight.Domain.Core.Interfaces;

namespace Artem_Kuznetsov
{
    public class TrafficLightProblem
    {
        private const int DigitsCount = 10;
        private readonly ITrafficLight _trafficlight;
        private readonly List<int[]> _iterations = new List<int[]>();
        private readonly List<int> _possibleAnswers = new List<int>(); 

        public TrafficLightProblem(ITrafficLight trafficlight)
        {
            if (trafficlight == null)
                throw new ArgumentException("Trafficlight is not initialized", "trafficlight");

            _trafficlight = trafficlight;
        }

        public void Solve()
        {
            while (_trafficlight.GetNext())
            {
                var leftDigits = GetPossibleDigits(_trafficlight.Current.Item1);
                var rightDigits = GetPossibleDigits(_trafficlight.Current.Item2);

                var possibleNumbers = CombineTwoDigits(leftDigits, rightDigits);
                _iterations.Add(possibleNumbers);

                var answer = CheckAnswer();
                if (!answer.HasValue)
                    continue;

                _trafficlight.Answer(answer.Value);
                return;
            }

            _trafficlight.Answer(0);
        }

        private static bool[] GetPossibleDigits(bool[] item)
        {
            var possibles = new bool[DigitsCount];
            for (var i = 0; i < DigitsCount; i++)
                possibles[i] = true;

            if (item[3])
                possibles[0] = false;

            if (item[0] || item[1] || item[3] || item[4] || item[6])
                possibles[1] = false;

            if (item[1] || item[5])
                possibles[2] = false;

            if (item[1] || item[4])
                possibles[3] = false;

            if (item[0] || item[4] || item[6])
                possibles[4] = false;

            if (item[2] || item[4])
                possibles[5] = false;

            if (item[2])
                possibles[6] = false;

            if (item[1] || item[3] || item[4] || item[6])
                possibles[7] = false;

            if (item[4])
                possibles[9] = false;

            return possibles;
        }

        private static int[] CombineTwoDigits(bool[] possiblesLeft, bool[] possiblesRight)
        {
            var result = new List<int>();

            for (var i = 0; i < DigitsCount; i++)
            {
                if (possiblesLeft[i])
                {
                    for (var j = 0; j < DigitsCount; j++)
                    {
                        if (possiblesRight[j])
                            result.Add(Convert.ToInt32(string.Concat(i, j)));
                    }
                }
            }

            return result.ToArray();
        }

        private int? CheckAnswer()
        {
            if (_iterations.Count == 1)
            {
                _possibleAnswers.AddRange(_iterations[0]);
                return _possibleAnswers.Count == 1 ? (int?)_possibleAnswers.First() : null;
            }

            var answers = _possibleAnswers.ToList();
            _possibleAnswers.Clear();
            foreach (var answer in answers)
            {
                if (_iterations.Last().Contains(answer - 1))
                    _possibleAnswers.Add(answer - 1);
            }

            return _possibleAnswers.Count == 1 ? (int?) _possibleAnswers.First() : null;
        }
    }
}
