using System;
using System.IO;
using TrafficLight.Domain.Core;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.DigitGenerator;
using TrafficLight.Domain.Core.DigitReaders;
using TrafficLight.Domain.Core.DigitReaders.MaskReader;

namespace TestApp
{
    class Program
    {
        static void DigitAnalyzerTest()
        {
            StreamReader sr = new StreamReader(@"test.txt");
            var digitEngine = new DigitEngine();
            var binaryMaskReader = new BinaryMaskReader();
            var integerMaskReader = new IntegerMaskReader();
            var digitReader = new StreamDigitReader(sr, binaryMaskReader);
            var trafficLightService = new TrafficLightService(digitReader);
            var digitAnalyzer = new TrafficLightAnalyzer(trafficLightService, digitEngine);
            var result = digitAnalyzer.Analyze();
            Console.WriteLine("Right answer is {0}", digitReader.GetRightAnsert());
            Console.WriteLine("Actual answer is {0} on step {1} after getting number {2}", result, digitReader.GetStep(), digitReader.GetLastNumber());
        }

        private static void DigitsGeneratorTest()
        {
            var digitGenerator = new DigitsGenerator();
            var result = digitGenerator.GenerateDigits(10);
        }

        static void Main(string[] args)
        {
           // DigitAnalyzerTest();
            DigitsGeneratorTest();

        }
    }
}
