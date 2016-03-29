using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrafficLight.Domain.Core;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.DigitGenerator;
using TrafficLight.Domain.Core.DigitReaders;
using TrafficLight.Domain.Core.DigitReaders.MaskReader;
using TrafficLight.Domain.Core.Filters;
using TrafficLight.Domain.Core.Helpers;
using TrafficLight.Domain.Core.Interfaces;

namespace TestApp
{
    class Program
    {
        static IDigitReader GenerateDigits(int number, string[] noises)
        {
            var digitGenerator = new DigitsGenerator();
            var digits = digitGenerator.GenerateDigits(number);
            
            if (noises != null)
            {
                var noiseGenerator = new NoiseGenerator(noises.ToList());
                digits = noiseGenerator.AddNoise(digits);
            }
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
            var trafficLightService = new Trafficlight(digitReader);
            var filters = new List<INumberFilter> { new SequenceDigitFilter(), new MaskDigitFilter() };
            var digitAnalyzer = new TrafficLightAnalyzer(trafficLightService, filters);
            var result = digitAnalyzer.Analyze();

            Console.WriteLine("Right answer is {0}", digitReader.GetRightAnswer());
            Console.WriteLine("Actual answer is {0} on step {1}", result, digitReader.GetStep());
            Console.WriteLine("First number is {0}", digitReader.GetFirstNumber());
        }

        static void Main(string[] args)
        {
            //var digitReader = ReadDigits("test.txt");
            var digitReader = GenerateDigits(55, new[] { "1010111", "1001001" });
            AnalyzeDigit(digitReader);
        }
    }
}
