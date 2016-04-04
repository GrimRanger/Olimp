using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace Trafficlight_Sudarkin
{
    public class Trafficlight_Sudarkin
    {
        private readonly Dictionary<int, bool[]> _digits = new Dictionary<int, bool[]>
        {
            {0, new[] { true,  true,  true,  false, true,  true,  true  } },
            {1, new[] { false, false, true,  false, false, true,  false } },
            {2, new[] { true,  false, true,  true,  true,  false, true  } },
            {3, new[] { true,  false, true,  true,  false, true,  true  } },
            {4, new[] { false, true,  true,  true,  false, true,  false } },
            {5, new[] { true,  true,  false, true,  false, true,  true  } },
            {6, new[] { true,  true,  false, true,  true,  true,  true  } },
            {7, new[] { true,  false, true,  false, false, true,  false } },
            {8, new[] { true,  true,  true,  true,  true,  true,  true  } },
            {9, new[] { true,  true,  true,  true,  false, true,  true  } }
        };

        public void SolveTrafficLightProblem(ITrafficLight trafficLight)
        {
            int fDigit = -1;
            int sDigit = -1;

            bool[,] workingCells = new bool[2, 7];

            List<List<int>[]> fSteps = new List<List<int>[]>();
            List<List<int>[]> sSteps = new List<List<int>[]>();

            while (trafficLight.GetNext())
            {
                Tuple<bool[], bool[]> current = trafficLight.Current;

                List<int>[] newWorkingCell =
                {
                    new List<int>(), new List<int>()
                };
                List<int>[] nonWorkingCell = 
                {
                    new List<int>(), new List<int>()
                };

                for (int i = 0; i < workingCells.GetLength(1); i++)
                {
                    if (current.Item1[i] && !workingCells[0, i])
                    {
                        workingCells[0, i] = true;
                        newWorkingCell[0].Add(i);
                    }
                    if (current.Item2[i] && !workingCells[1, i])
                    {
                        workingCells[1, i] = true;
                        newWorkingCell[1].Add(i);
                    }
                    if (!workingCells[0, i]) { nonWorkingCell[0].Add(i); }
                    if (!workingCells[1, i]) { nonWorkingCell[1].Add(i); }
                }

                if (sDigit > -1)
                {
                    sDigit = (sDigit != 0) ? (sDigit - 1) : 9;
                }
                else
                {
                    sSteps.Add(new[] { GetPossibleDigits(current.Item2, workingCells, 1), nonWorkingCell[1] });
                    if (sSteps.Count > 1)
                    {
                        TransformSteps(ref sSteps, ref sDigit, newWorkingCell[1], true);
                    }
                }

                List<int> newPosDig = GetPossibleDigits(current.Item1, workingCells, 0);
                if (fSteps.Count > 0)
                {
                    if (!newPosDig.SequenceEqual(fSteps[fSteps.Count - 1][0]))
                    {
                        if (fDigit <= 0)
                        {
                            fSteps.Add(new[] { newPosDig, nonWorkingCell[0] });
                            TransformSteps(ref fSteps, ref fDigit, newWorkingCell[0]);
                        }
                        else
                        {
                            fDigit--;
                        }
                    }
                }
                else
                {
                    fSteps.Add(new[] { newPosDig, nonWorkingCell[0] });
                }
                if (fDigit > -1 && sDigit > -1) { break; }
            }
            int result = (fDigit > -1 && sDigit > -1) ? (fDigit * 10 + sDigit) : 0;
            trafficLight.Answer(result);
        }
        /// <summary>
        /// Возвращает список возможных вариантов показанной цифры
        /// </summary>
        /// <param name="digit">Цифра</param>
        /// <param name="workingCells">Матрица, с информацией о работающих ячейках</param>
        /// <param name="pos">Позиция цифры в числе</param>
        private List<int> GetPossibleDigits(IEnumerable<bool> digit, bool[,] workingCells, int pos)
        {
            Dictionary<int, int> weights = _digits.ToDictionary(d => d.Key,
                d => d.Value.Where((t, j) => t == digit.ToArray()[j] || !workingCells[pos, j]).Count());
            return (from x in weights where x.Value == 7 select x.Key).ToList();
        }
        /// <summary>
        /// Преобразует список шагов на основе новых данных
        /// </summary>
        /// <param name="steps">Список шагов</param>
        /// <param name="digit">Куда записать цифру в случае однозначного распознавания</param>
        /// <param name="newWc">Список с информацией о новых работающих ячейках</param>
        /// <param name="zeroToNine">Поддерживается ли переход 0 -> 9</param>
        private void TransformSteps(ref List<List<int>[]> steps, ref int digit, List<int> newWc, bool zeroToNine = false)
        {
            foreach (List<int>[] step in steps)
            {
                foreach (int index in step[1].Intersect(newWc))
                {
                    step[0] = step[0].Where(dig => !_digits[dig][index]).ToList();
                }
            }
            for (int i = 1; i < steps.Count; i++)
            {
                List<int> step = new List<int>();
                foreach (int item in steps[i][0])
                {
                    if ((zeroToNine && item == 9 && steps[i - 1][0].Contains(0)) || steps[i - 1][0].Contains(item + 1))
                    {
                        step.Add(item);
                    }
                }
                steps[i][0] = step;
            }
            if (steps[steps.Count - 1][0].Count == 1)
            {
                digit = steps[steps.Count - 1][0][0];
            }
        }
    }
}
