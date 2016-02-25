using System;
using System.Collections.Generic;
using System.IO;
using TrafficLight.Domain.Core;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.DigitGenerator;
using TrafficLight.Domain.Core.DigitReaders;
using TrafficLight.Domain.Core.DigitReaders.MaskReader;
using TrafficLight.Domain.Core.Filters;
using TrafficLight.Domain.Core.Interfaces;

namespace TestApp
{
    class Program
    {
        static IDigitReader GenerateDigits(int number)
        {

            var digitGenerator = new DigitsGenerator();
            var digits = digitGenerator.GenerateDigits(number);
            var digitReader = new DigitStorageReader(digits, number);

            return digitReader;
        }

        static IDigitReader ReadDigits(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);

            var binaryMaskReader = new BinaryMaskReader();
            //var integerMaskReader = new IntegerMaskReader();
            var digitReader = new StreamDigitReader(sr, binaryMaskReader);

            return digitReader;
        }

        static void AnalyzeDigit(IDigitReader digitReader)
        {
            var trafficLightService = new TrafficLightService(digitReader);
            var filters = new List<INumberFilter> {new SequenceDigitFilter(), new MaskDigitFilter()};
            var digitEngine = new DigitEngine();
            var digitAnalyzer = new TrafficLightAnalyzer(trafficLightService, digitEngine, filters);
            var result = digitAnalyzer.Analyze();
            Console.WriteLine("Right answer is {0}", digitReader.GetRightAnsert());
            Console.WriteLine("Actual answer is {0} on step {1} after getting number {2}", result, digitReader.GetStep(), digitReader.GetLastNumber());
        }

        static void Main(string[] args)
        {
            //var digitReader = ReadDigits("test.txt");
            var digitReader = GenerateDigits(25);
            AnalyzeDigit(digitReader);

            //var maskFilter = new MaskDigitFilter();

           // var result = maskFilter.CheckDigit(127, 93, 93);
            //Console.Write(result);
        }
    }
}
