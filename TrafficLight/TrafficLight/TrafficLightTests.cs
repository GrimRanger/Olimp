using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TrafficLight.Domain.Core;
using TrafficLight.Domain.Core.DigitGenerator;
using TrafficLight.Domain.Core.DigitReaders;
using TrafficLight.Domain.Core.Filters;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight
{
    public class TrafficLightTests
    {
        public Dictionary<string, Action<ITrafficLight>> ContestantSolves()
        {
            var listActionITrafficLight = new Dictionary<string,Action<ITrafficLight>>();

            var Sharkunova = new Anna_Sharkunova.Anna_Sharkunova();
            listActionITrafficLight.Add("Sharkunova", Sharkunova.SolveTrafficLightProblem);

            var Fedorov = new Andrey_Fedorov.Andrey_Fedorov();
            listActionITrafficLight.Add("Fedorov", Fedorov.SolveTrafficLightProblem);

            // var Alexeev_Sergey = new Sergey_Alexeev.Sergey_Alexeev();
            listActionITrafficLight.Add("Alexeev", Sergey_Alexeev.Sergey_Alexeev.SolveTrafficLightProblem);

            // var Ionov_Peter = new Peter_Ionov.Peter_Ionov();
            listActionITrafficLight.Add("Ionov", Peter_Ionov.Peter_Ionov.SolveTrafficLightProblem);

            // var Andrei_Makeyev = new Andrei_Makeyev.TrafficLightDigit();
            listActionITrafficLight.Add("Makeyev", Andrei_Makeyev.TrafficLightProcessor.GetCurrentState);

            var Kozlova = new Anna_Kozlova.Problem();
            listActionITrafficLight.Add("Kozlova", Kozlova.SolveTrafficLightProblem);

            // var Kalachev_Dmitry = new KarachevProject.Dmitry_Kalachev();
            listActionITrafficLight.Add("Kalachev", KarachevProject.Dmitry_Kalachev.Method);

            //var Koroleva_Irina = new Irina_Koroleva.Irina_Koroleva();
            listActionITrafficLight.Add("Koroleva", Irina_Koroleva.Irina_Koroleva.SolveTrafficLightProblem);

            var Golubtsov = new Vasily_Golubtsov.StateDetector();
            listActionITrafficLight.Add("Golubtsov", Golubtsov.SolveTrafficLightProblem);

            var Belov = new Alexander_Belov.TrafficLightProblemSolver();
            listActionITrafficLight.Add("Belov", Belov.SolveTrafficLightProblem);

            var Kuznetsov = new Artem_Kuznetsov.TrafficLightProblem(null);
            listActionITrafficLight.Add("Kuznetsov", Kuznetsov.Solve);

            //var Galaida_Anna = new Traffic2.Anna_Galaida();
            listActionITrafficLight.Add("Galaida", Traffic2.Anna_Galaida.SolveTrafficLightProblem);

            //var Banokin = new Banokin_KB21_SolveTrafficLightProblem.Banokin_KB21();
            listActionITrafficLight.Add("Banokin", Banokin_KB21_SolveTrafficLightProblem.Banokin_KB21.SolveTrafficLightProblem);

            //var IlyashenkoK = new IlyashenkoKB21.IlyashenkoKB21();
            listActionITrafficLight.Add("IlyashenkoK", IlyashenkoKB21.IlyashenkoKB21.SolveTrafficLightProblem);

            var Sudarkin = new Trafficlight_Sudarkin.Trafficlight_Sudarkin();
            listActionITrafficLight.Add("Sudarkin", Sudarkin.SolveTrafficLightProblem);

            //var Volyakov = new Volyakov.Volyakov();
            listActionITrafficLight.Add("Volyakov", Volyakov.Volyakov.TrafficlightProblem);

            //var Zhuchenko = new Zhuchenko_Kristina.Zhuchenko_Kristina();
            listActionITrafficLight.Add("Zhuchenko", Zhuchenko_Kristina.Zhuchenko_Kristina.SolveTrafficLightProblem);

            //var Alexander_Efimov = new Alexander_Efimov.Alexander_Efimov();
             //listActionITrafficLight.Add("Zhuchenko", Zhuchenko_Kristina.Zhuchenko_Kristina.);

           // var Mironych = new Alexander_Mironych.StatusDeterminer();
             listActionITrafficLight.Add("Mironych", Alexander_Mironych.StatusDeterminer.Determine);

            //var Nikitin = new Алексей_Никитин.Alexei_Nikitin();
             listActionITrafficLight.Add("Nikitin", Алексей_Никитин.Alexei_Nikitin.Slove);

            return listActionITrafficLight;
        }

        public IDigitReader GenerateDigits(int number, int digitCount, string[] noises)
        {
            var digitGenerator = new DigitsGenerator();
            var digits = digitGenerator.GenerateDigits(number, digitCount);
            if (noises != null)
            {
                var noiseGenerator = new NoiseGenerator(noises.ToList());
                digits = noiseGenerator.AddNoise(digits);
            }
            var digitReader = new DigitStorageReader(digits, number);

            return digitReader;
        }

        [TestCase(1, 1, null, TestName = "Test 1")]
        [TestCase(5, 1, null, TestName = "Test 2")]
        [TestCase(9, 1, null, TestName = "Test 3")]

        [TestCase(10, 2, null, TestName = "Test 4")]
        [TestCase(19, 2, null, TestName = "Test 5")]
        [TestCase(25, 2, null, TestName = "Test 6")]
        [TestCase(42, 2, null, TestName = "Test 7")]

        [TestCase(100, 3, null, TestName = "Test 8")]
        [TestCase(125, 3, null, TestName = "Test 9")]
        [TestCase(500, 3, null, TestName = "Test 10")]
        [TestCase(666, 3, null, TestName = "Test 11")]
        [TestCase(777, 3, null, TestName = "Test 12")]
        [TestCase(900, 3, null, TestName = "Test 13")]

        [TestCase(9, 1, new[] { "0000000" }, TestName = "Test 14")]
        [TestCase(8, 1, new[] { "1001001" }, TestName = "Test 15")]
        [TestCase(9, 1, new[] { "1010111" }, TestName = "Test 16")]
        [TestCase(7, 1, new[] { "0010000" }, TestName = "Test 17")]
        [TestCase(5, 1, new[] { "0111100" }, TestName = "Test 18")]

        [TestCase(10, 2, new[] { "0000000", "0000000" }, TestName = "Test 19")]
        [TestCase(10, 2, new[] { "1111111", "1111111" }, TestName = "Test 20")]
        [TestCase(15, 2, new[] { "1001001", "1001001" }, TestName = "Test 21")]
        [TestCase(15, 2, new[] { "1110000", "0000111" }, TestName = "Test 22")]
        [TestCase(25, 2, new[] { "1010111", "1001001" }, TestName = "Test 23")]
        [TestCase(25, 2, new[] { "0111110", "1000001" }, TestName = "Test 24")]
        [TestCase(88, 2, new[] { "0010000", "1001001" }, TestName = "Test 25")]
        [TestCase(88, 2, new[] { "1001100", "0110011" }, TestName = "Test 26")]
        [TestCase(99, 2, new[] { "0111100", "1001001" }, TestName = "Test 27")]

        [TestCase(905, 3, new[] { "0000000", "1001001", "1001001" }, TestName = "Test 28")]
        [TestCase(800, 3, new[] { "1001001", "1001001", "1001001" }, TestName = "Test 29")]
        [TestCase(800, 3, new[] { "0000000", "1111111", "0000000" }, TestName = "Test 30")]
        [TestCase(800, 3, new[] { "1111111", "0000000", "1111111" }, TestName = "Test 31")]
        [TestCase(800, 3, new[] { "1001001", "1001001", "1001001" }, TestName = "Test 32")]
        [TestCase(666, 3, new[] { "1010111", "1001001", "1001001" }, TestName = "Test 33")]
        [TestCase(100, 3, new[] { "0010000", "1001001", "1001001" }, TestName = "Test 34")]
        [TestCase(300, 3, new[] { "0111100", "1001001", "1001001" }, TestName = "Test 35")]
        public void TrafficLightAnalyzer_Analyze_CapacityAndCountAreEqual_ResultShouldBeTrue(int number, int digitCount, string[] noises)
        {
            var digitReader = GenerateDigits(number, digitCount, noises);
            var trafficLightService = new Domain.Core.TrafficLight(digitReader);
            var filters = new List<INumberFilter> { new SequenceDigitFilter(), new MaskDigitFilter() };
            var digitAnalyzer = new TrafficLightAnalyzer(trafficLightService, filters);

            var actualResult = digitAnalyzer.Analyze();
            var expectedResult = digitReader.GetRightAnswer();
            Assert.AreEqual(expectedResult, actualResult);

            var expectedStepCount = digitReader.GetStep();
            var actualStepCount = digitReader.GetStep();
            Assert.AreEqual(expectedStepCount, actualStepCount);
        }
    }
}
