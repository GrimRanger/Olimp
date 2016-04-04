using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace Alexander_Belov
{
  
    public class TrafficLightProblemSolver
    {
        private const int NumberOfSectors=7;
        private const int MaxNumber = 99;

        private Dictionary<int, int[]> _digitsAssociatedWithSectors;
        private bool[] _workingSectorsOnLeftTable;
        private bool[] _workingSectorsOnRightTable;



        public TrafficLightProblemSolver()
        {
            _workingSectorsOnLeftTable = new bool[NumberOfSectors];
            _workingSectorsOnRightTable = new bool[NumberOfSectors];

            _digitsAssociatedWithSectors = new Dictionary<int, int[]>()
            {
                {0, new[] {0, 2, 3, 5, 6, 7, 8, 9}},
                {1, new[] {0, 4, 5, 6, 8, 9}},
                {2, new[] {0, 1, 2, 3, 4, 7, 8, 9}},
                {3, new[] {2, 3, 4, 5, 6, 8, 9}},
                {4, new[] {0, 2, 6, 8}},
                {5, new[] {0, 1, 3, 4, 5, 6, 7, 8, 9}},
                {6, new[] {0, 2, 3, 5, 6, 8, 9}}
            };

        }

        public void SolveTrafficLightProblem(ITrafficLight trafficLight)
        {
            var result = 0;
            var countdownIsOver = !trafficLight.GetNext();
            var possibleNumbers=new List<int>();


            for (int i = 0; i <= MaxNumber; i++)
            {
                possibleNumbers.Add(i);
            }


            while (!countdownIsOver)
            {
                possibleNumbers = GetPosibleNumbersOnCurrentStep(trafficLight.Current,possibleNumbers);
                if (possibleNumbers.Count == 1)
                {
                    result = possibleNumbers[0];
                    break;
                }
                DecreaseNumbers(possibleNumbers);
                countdownIsOver = !trafficLight.GetNext();
            }
            trafficLight.Answer(result); 
        }

        private void DecreaseNumbers(List<int> possibleNumbers)
        {
            for (int i = 0; i < possibleNumbers.Count; i++)
            {
                possibleNumbers[i]--;
            }
        }

        private List<int> GetPosibleNumbersOnCurrentStep(Tuple<bool[], bool[]> trafficLightSectors, List<int> possibleNumbersOnPreviousStep)
        {
            var possibleDigitsOnRightTable = new HashSet<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var possibleDigitsOnLeftTable = new HashSet<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < 7; i++)
            {
                AnalyzeSector(i,trafficLightSectors.Item1[i],_workingSectorsOnLeftTable,possibleDigitsOnLeftTable);
                AnalyzeSector(i, trafficLightSectors.Item2[i], _workingSectorsOnRightTable, possibleDigitsOnRightTable);
            }

            var numbers= GetAllCombinations(possibleDigitsOnLeftTable,possibleDigitsOnRightTable);
            numbers.IntersectWith(possibleNumbersOnPreviousStep);
            return numbers.ToList();

        }

        private void AnalyzeSector(int sectorNumber,bool sectorValue,bool[] workingSectors,HashSet<int> posibleDigits)
        {
            if (sectorValue)
            {
                workingSectors[sectorNumber] = true;
                posibleDigits.IntersectWith(_digitsAssociatedWithSectors[sectorNumber]);
            }
            else
            {
                if (workingSectors[sectorNumber])
                {
                    posibleDigits.ExceptWith(_digitsAssociatedWithSectors[sectorNumber]);
                }
            }
        }

        private HashSet<int> GetAllCombinations(HashSet<int> possibleDigitsOnLeftTable, HashSet<int> possibleDigitsOnRightTable)
        {
            var resultNumbers=new HashSet<int>();

            foreach (var digitOnLeftTable in possibleDigitsOnLeftTable)
            {
                var currentDecade = digitOnLeftTable*10;
                foreach (var leastSignificantDigit in possibleDigitsOnRightTable)
                {
                    resultNumbers.Add(currentDecade+leastSignificantDigit);
                }
            }
            return resultNumbers;
        }
    }
}
