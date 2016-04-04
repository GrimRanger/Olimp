using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace Anna_Kozlova
{
    internal class TrafficLightNumber
    {
        public int Value { get; private set; }
 
        public bool[] WorkingBits { get; private set; }

        public bool[] BrokenBits { get; private set; }

        public TrafficLightNumber(int value, bool[] workingBits, bool[] brokenBits)
        {
            Value = value;
            WorkingBits = workingBits;
            BrokenBits = brokenBits;
        }
    }

    public class Problem
    {
        private readonly bool[][] _template = {new bool[]{true, true, true, false, true, true, true},//0
                                              new bool[]{false, false, true, false, false, true, false},//1
                                              new bool[]{true, false, true, true, true, false, true},//2
                                              new bool[]{true, false, true, true, false, true, true},//3
                                              new bool[]{false, true, true, true, false, true, false},//4
                                              new bool[]{true, true, false, true, false, true, true},//5                                              
                                              new bool[]{true, true, false, true, true, true, true},//6
                                              new bool[]{true, false, true, false, false, true, false},//7
                                              new bool[]{true, true, true, true, true, true, true},//8
                                              new bool[]{true, true, true, true, false, true, true},//9
                                             };

        private bool[] Or(bool[] item1, bool[] item2)
        {
            var temp = new bool[item1.Length];
            for (var i = 0; i < item1.Length; i++)
            {
                temp[i] = item1[i] || item2[i];
            }
            return temp;
        }

        private bool[] And(bool[] item1, bool[] item2)
        {
            var temp = new bool[item1.Length];
            for (var i = 0; i < item1.Length; i++)
            {
                temp[i] = item1[i] && item2[i];
            }
            return temp;
        }

        private bool[] Xor(bool[] item1, bool[] item2)
        {
            var temp = new bool[item1.Length];
            for (var i = 0; i < item1.Length; i++)
            {
                temp[i] = item1[i] ^ item2[i];
            }
            return temp;
        }

        private bool[] Not(bool[] item)
        {
            var temp = new bool[item.Length];
            for (var i = 0; i < item.Length; i++)
            {
                temp[i] = !item[i];
            }
            return temp;
        }

        private bool IsFirstItemSubsetOfSecondItem(bool[] item1, bool[] item2)
        {
            return Xor(Or(item1, item2), item2).All(x => !x);
        }

        public Problem() { }

        public void SolveTrafficLightProblem(ITrafficLight trafficLight)
        {
            var firstNumber = -1;
            var secondNumber = -1;

            var supposedNumbers1 = new List<List<TrafficLightNumber>>();
            var supposedNumbers2 = new List<List<TrafficLightNumber>>();

            trafficLight.GetNext();

            var item1 = trafficLight.Current.Item1;
            var item2 = trafficLight.Current.Item2;
            
            for (var i = 0; i < _template.Length; i++)
            {
                var template = _template[i];
                var IsSuitableForTemplate1 = IsFirstItemSubsetOfSecondItem(item1, template);
                if (IsSuitableForTemplate1)
                {
                    var workingBits = And(item1, template);
                    var brokenBits = And(Not(item1), template);
                    supposedNumbers1.Add(new List<TrafficLightNumber>() { new TrafficLightNumber(i, workingBits, brokenBits) });
                }

                var IsSuitableForTemplate2 = IsFirstItemSubsetOfSecondItem(item2, template);
                if (IsSuitableForTemplate2)
                {                  
                    var workingBits = And(item2, template);
                    var brokenBits = And(Not(item2), template);
                    supposedNumbers2.Add(new List<TrafficLightNumber>() { new TrafficLightNumber(i, workingBits, brokenBits) });
                }
            }
            if (supposedNumbers1.Count == 1)
            {
                firstNumber = supposedNumbers1[0][0].Value;
            }

            if (supposedNumbers2.Count == 1)
            {
                secondNumber = supposedNumbers2[0][0].Value;
            }


            while (firstNumber < 0 || secondNumber < 0)
            {
                if (!trafficLight.GetNext())
                {
                    firstNumber = secondNumber = 0;
                }
                else
                {
                    var currentNumber1 = trafficLight.Current.Item1;
                    var currentNumber2 = trafficLight.Current.Item2;

                    var isFirstNumberChanged = !trafficLight.Current.Item1.SequenceEqual(item1);

                    if (firstNumber >= 0)
                    {
                        firstNumber = isFirstNumberChanged ? firstNumber - 1 : firstNumber;
                    }
                    else
                    {
                        if(isFirstNumberChanged)
                        {
                            var removedIndexes1 = new List<int>();
                            for (var i = 0; i < supposedNumbers1.Count; i++)
                            {
                                var lastIndex = supposedNumbers1[i].Count - 1;
                                var number = supposedNumbers1[i][lastIndex];
                                var index = number.Value - 1;
                                if (index < 0)
                                {
                                    removedIndexes1.Add(i);
                                    continue;
                                }
                                var template = _template[index];
                                var IsSuitableForTemplate =
                                    // Те ячейки, что работают и не должны светиться согласно шаблону, отсутствуют в текущей цифре
                                    And(And(number.WorkingBits, Not(template)), currentNumber1).All(x => !x) &&
                                    // Рабочие ячейки, светящиеся в шаблоне, светятся в текущей цифре
                                    IsFirstItemSubsetOfSecondItem(And(number.WorkingBits, template), currentNumber1) &&
                                    // Неработающие ячейки не светятся в текущей цифре
                                    And(currentNumber1, number.BrokenBits).All(x => !x) &&
                                    // Подходит ли текущая цифра шаблону
                                    IsFirstItemSubsetOfSecondItem(currentNumber1, template);
                                if (IsSuitableForTemplate)
                                {
                                    var workingBits = Or(And(currentNumber1, template), number.WorkingBits);
                                    var brokenBits = Or(And(Not(currentNumber1), template), number.BrokenBits);
                                    supposedNumbers1[i].Add(new TrafficLightNumber(index, workingBits, brokenBits));
                                }
                                else
                                {
                                    removedIndexes1.Add(i);
                                }
                            }
                            removedIndexes1.Reverse();
                            foreach (var index in removedIndexes1)
                            {
                                supposedNumbers1.RemoveAt(index);
                            }
                            if (supposedNumbers1.Count == 1)
                            {
                                firstNumber = supposedNumbers1[0][supposedNumbers1[0].Count - 1].Value;
                            }
                        }
                    }
                    if (secondNumber >= 0)
                    {
                        secondNumber = (secondNumber - 1) >= 0 ? (secondNumber - 1) : 9;
                    }
                    else
                    {
                        if (isFirstNumberChanged)
                        {
                            secondNumber = 9;
                        }
                        else
                        {
                            var removedIndexes2 = new List<int>();
                            for (var i = 0; i < supposedNumbers2.Count; i++)
                            {
                                var lastIndex = supposedNumbers2[i].Count - 1;
                                var number = supposedNumbers2[i][lastIndex];
                                var index = (number.Value - 1 >= 0) ? number.Value - 1 : 9;
                                var template = _template[index];
                                var IsSuitableForTemplate =
                                    And(And(number.WorkingBits, Not(template)), currentNumber2).All(x => !x) &&
                                    IsFirstItemSubsetOfSecondItem(And(number.WorkingBits, template), currentNumber2) && 
                                    And(currentNumber2, number.BrokenBits).All(x => !x) && 
                                    IsFirstItemSubsetOfSecondItem(currentNumber2, template);
                                if (IsSuitableForTemplate)
                                {
                                    var workingBits = Or(And(currentNumber2, template), number.WorkingBits);
                                    var brokenBits = Or(And(Not(currentNumber2), template), number.BrokenBits);
                                    supposedNumbers2[i].Add(new TrafficLightNumber(index, workingBits, brokenBits));
                                }
                                else
                                {
                                    removedIndexes2.Add(i);
                                }
                            }
                            removedIndexes2.Reverse();
                            foreach (var index in removedIndexes2)
                            {
                                supposedNumbers2.RemoveAt(index);
                            }
                            if (supposedNumbers2.Count == 1)
                            {
                                secondNumber = supposedNumbers2[0][supposedNumbers2[0].Count - 1].Value;
                            }
                        }
                    }
                }
                item1 = trafficLight.Current.Item1;
                item2 = trafficLight.Current.Item2;
            }

            trafficLight.Answer(firstNumber * 10 + secondNumber);
        }  
    }
}
