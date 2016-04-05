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
    public partial class TrafficLightTests
    {
        public static IDigitReader GenerateDigits(int number, int digitCount, string[] noises)
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

        [TestCase(1, 1, null, TestName = "digitAnalyzerTest 1")]
        [TestCase(5, 1, null, TestName = "digitAnalyzerTest 2")]
        [TestCase(9, 1, null, TestName = "digitAnalyzerTest 3")]

        [TestCase(10, 2, null, TestName = "digitAnalyzerTest 4")]
        [TestCase(19, 2, null, TestName = "digitAnalyzerTest 5")]
        [TestCase(25, 2, null, TestName = "digitAnalyzerTest 6")]
        [TestCase(42, 2, null, TestName = "digitAnalyzerTest 7")]

        [TestCase(100, 3, null, TestName = "digitAnalyzerTest 8")]
        [TestCase(125, 3, null, TestName = "digitAnalyzerTest 9")]
        [TestCase(500, 3, null, TestName = "digitAnalyzerTest 10")]
        [TestCase(666, 3, null, TestName = "digitAnalyzerTest 11")]
        [TestCase(777, 3, null, TestName = "digitAnalyzerTest 12")]
        [TestCase(900, 3, null, TestName = "digitAnalyzerTest 13")]

        [TestCase(9, 1, new[] { "0000000" }, TestName = "digitAnalyzerTest 14")]
        [TestCase(8, 1, new[] { "1001001" }, TestName = "digitAnalyzerTest 15")]
        [TestCase(9, 1, new[] { "1010111" }, TestName = "digitAnalyzerTest 16")]
        [TestCase(7, 1, new[] { "0010000" }, TestName = "digitAnalyzerTest 17")]
        [TestCase(5, 1, new[] { "0111100" }, TestName = "digitAnalyzerTest 18")]

        [TestCase(10, 2, new[] { "0000000", "0000000" }, TestName = "digitAnalyzerTest 19")]
        [TestCase(10, 2, new[] { "1111111", "1111111" }, TestName = "digitAnalyzerTest 20")]
        [TestCase(15, 2, new[] { "1001001", "1001001" }, TestName = "digitAnalyzerTest 21")]
        [TestCase(15, 2, new[] { "1110000", "0000111" }, TestName = "digitAnalyzerTest 22")]
        [TestCase(25, 2, new[] { "1010111", "1001001" }, TestName = "digitAnalyzerTest 23")]
        [TestCase(25, 2, new[] { "0111110", "1000001" }, TestName = "digitAnalyzerTest 24")]
        [TestCase(88, 2, new[] { "0010000", "1001001" }, TestName = "digitAnalyzerTest 25")]
        [TestCase(88, 2, new[] { "1001100", "0110011" }, TestName = "digitAnalyzerTest 26")]
        [TestCase(99, 2, new[] { "0111100", "1001001" }, TestName = "digitAnalyzerTest 27")]

        [TestCase(905, 3, new[] { "0000000", "1001001", "1001001" }, TestName = "digitAnalyzerTest 28")]
        [TestCase(800, 3, new[] { "1001001", "1001001", "1001001" }, TestName = "digitAnalyzerTest 29")]
        [TestCase(800, 3, new[] { "0000000", "1111111", "0000000" }, TestName = "digitAnalyzerTest 30")]
        [TestCase(800, 3, new[] { "1111111", "0000000", "1111111" }, TestName = "digitAnalyzerTest 31")]
        [TestCase(800, 3, new[] { "1001001", "1001001", "1001001" }, TestName = "digitAnalyzerTest 32")]
        [TestCase(666, 3, new[] { "1010111", "1001001", "1001001" }, TestName = "digitAnalyzerTest 33")]
        [TestCase(100, 3, new[] { "0010000", "1001001", "1001001" }, TestName = "digitAnalyzerTest 34")]
        [TestCase(300, 3, new[] { "0111100", "1001001", "1001001" }, TestName = "digitAnalyzerTest 35")]
        public void TrafficLightAnalyzer_Analyze_CapacityAndCountAreEqual_ResultShouldBeTrue(int number, int digitCount, string[] noises)
        {
            var digitReader = GenerateDigits(number, digitCount, noises);
            var trafficLightService = new Domain.Core.TrafficLight(digitReader);
            var filters = new List<INumberFilter> { new SequenceDigitFilter(), new MaskDigitFilter(), new MaskChangeDigitFilter() };
            var digitAnalyzer = new TrafficLightAnalyzer(filters);

            digitAnalyzer.Analyze(trafficLightService);
            var actualResult = trafficLightService.GetResult();

            var rightAnswer = digitReader.GetRightAnswer();
            Assert.AreEqual(rightAnswer, actualResult.UserAnswer, "Test Name : {0} \r\n Answer : {1}", NUnit.Framework.TestContext.CurrentContext.Test.Name, rightAnswer);
            Assert.AreEqual(actualResult.RightAnswer, actualResult.UserAnswer);

            System.Diagnostics.Debug.WriteLine("Test Name : {0} \r\n Answer : {1}", NUnit.Framework.TestContext.CurrentContext.Test.Name, rightAnswer);
            var expectedStepCount = digitReader.GetStep();
            var actualStepCount = digitReader.GetStep();
            Assert.AreEqual(expectedStepCount, actualStepCount);
        }

        [TestCase(10, 2, null, TestName = "SimpleTestOne 1")]
        [TestCase(19, 2, null, TestName = "SimpleTestOne 2")]
        [TestCase(25, 2, null, TestName = "SimpleTestOne 3")]
        [TestCase(42, 2, null, TestName = "SimpleTestOne 4")]

        [TestCase(10, 2, new[] { "0000000", "0000000" }, TestName = "SimpleTestOne 5")]
        [TestCase(10, 2, new[] { "1111111", "1111111" }, TestName = "SimpleTestOne 6")]
        [TestCase(15, 2, new[] { "1001001", "1001001" }, TestName = "SimpleTestOne 7")]
        [TestCase(15, 2, new[] { "1110000", "0000111" }, TestName = "SimpleTestOne 8")]
        [TestCase(25, 2, new[] { "1010111", "1001001" }, TestName = "SimpleTestOne 9")]
        [TestCase(25, 2, new[] { "0111110", "1000001" }, TestName = "SimpleTestOne 10")]
        [TestCase(88, 2, new[] { "0010000", "1001001" }, TestName = "SimpleTestOne 11")]
        [TestCase(88, 2, new[] { "1001100", "0110011" }, TestName = "SimpleTestOne 12")]
        [TestCase(99, 2, new[] { "0111100", "1001001" }, TestName = "SimpleTestOne 13")]
        [TestCase(25, 2, new[] { "0111111", "0000000" }, TestName = "ExpectedResult 10")]
        [TestCase(19, 2, new[] { "0111111", "0000000" }, TestName = "ExpectedResult 10")]
        public void TrafficLightAnalyzer_Analyze_SimpleTestOneContestants(int number, int digitCount, string[] noises)
        {
            var filters = new List<INumberFilter> { new SequenceDigitFilter(), new MaskDigitFilter(), new MaskChangeDigitFilter() };
            var digitAnalyzer = new TrafficLightAnalyzer(filters);

            var actualResult = TrafficLightAnalyzer_Test(Andrei_Makeyev.TrafficLightProcessor.GetCurrentState, number, noises);

            Assert.AreEqual(actualResult.RightAnswer, actualResult.UserAnswer);
        }

        [TestCase(10, 2, null, TestName = "ExpectedResult 1")]
        [TestCase(19, 2, null, TestName = "ExpectedResult 2")]
        [TestCase(25, 2, null, TestName = "ExpectedResult 3")]
        [TestCase(42, 2, null, TestName = "ExpectedResult 4")]

        [TestCase(10, 2, new[] { "0000000", "0000000" }, TestName = "ExpectedResult 5")]
        [TestCase(10, 2, new[] { "1111111", "1111111" }, TestName = "ExpectedResult 6")]
        [TestCase(15, 2, new[] { "1001001", "1001001" }, TestName = "ExpectedResult 7")]
        [TestCase(15, 2, new[] { "1110000", "0000111" }, TestName = "ExpectedResult 8")]
        [TestCase(25, 2, new[] { "1010111", "1001001" }, TestName = "ExpectedResult 9")]
        [TestCase(25, 2, new[] { "0111110", "1000001" }, TestName = "ExpectedResult 10")]
        [TestCase(25, 2, new[] { "0111111", "0000000" }, TestName = "ExpectedResult 10")]

        [TestCase(88, 2, new[] { "0010000", "1001001" }, TestName = "ExpectedResult 11")]
        [TestCase(88, 2, new[] { "1001100", "0110011" }, TestName = "ExpectedResult 12")]
        [TestCase(99, 2, new[] { "0111100", "1001001" }, TestName = "ExpectedResult 13")]
        public void TrafficLightAnalyzer_Analyze_TestContestantsWithExpectedResult(int number, int digitCount, string[] noises)
        {
            var filters = new List<INumberFilter> { new SequenceDigitFilter(), new MaskDigitFilter() };
            var digitAnalyzer = new TrafficLightAnalyzer(filters);

            var actualResult = TrafficLightAnalyzer_Test(Volyakov.Volyakov.TrafficlightProblem, number, noises);
            var expectedResult = TrafficLightAnalyzer_Test(digitAnalyzer.Analyze, number, noises);

            TestContext.WriteLine(string.Format("expected result : {0}", expectedResult.RightAnswer));
            TestContext.WriteLine(string.Format("tester result : {0}", expectedResult.UserAnswer));
            TestContext.WriteLine(string.Format("actual result : {0}", actualResult.UserAnswer));
            TestContext.WriteLine(string.Format("expected steps : {0}", expectedResult.Steps));
            TestContext.WriteLine(string.Format("actual steps : {0}", actualResult.Steps));
            Assert.AreEqual(expectedResult.RightAnswer, expectedResult.UserAnswer);
            Assert.AreEqual(expectedResult.UserAnswer, actualResult.UserAnswer);
            Assert.AreEqual(expectedResult.Steps, actualResult.Steps);
        }

        public static TrafficLightResult TrafficLightAnalyzer_Test(Action<ITrafficLight> solveTrafficProblem, int number, string[] noises)
        {
            var digitReader = GenerateDigits(number, 2, noises);
            var trafficLightService = new Domain.Core.TrafficLight(digitReader);

            solveTrafficProblem(trafficLightService);

            return trafficLightService.GetResult();
        }
    }
}
