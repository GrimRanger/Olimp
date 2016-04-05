using System;
using NUnit.Framework;
using TrafficlightApp;
using System.Collections;

namespace Tests
{
    [TestFixture]
    public class TrafficlightProblemTests
    {
        private static readonly ILogger Logger = new Logger();

        [TearDown]
        public void Dispose()
        {
            Logger.Clear();
        }

        [Test, TestCaseSource("TestCases")]
        public void CommonTest(ITrafficlight trafficlight, string expected)
        {
            var problem = new TrafficLightProblem(trafficlight);

            problem.Solve();

            Assert.AreEqual(expected, Logger.ToString());
        }


        private static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(
                    new Trafficlight(
                        86,
                        new Tuple<bool[], bool[]>(
                            new[] {true, true, true, true, true, true, true},
                            new[] {true, true, false, true, false, true, true}),
                        Logger),
                    "84");
                yield return new TestCaseData(
                    new Trafficlight(
                        99,
                        new Tuple<bool[], bool[]>(
                            new[] {false, false, false, false, false, false, false},
                            new[] {false, false, false, false, false, false, false}),
                        Logger),
                    "0");
                yield return new TestCaseData(
                    new Trafficlight(
                        9,
                        new Tuple<bool[], bool[]>(
                            new[] {true, true, true, true, true, true, true},
                            new[] {true, true, true, true, true, true, true}),
                        Logger),
                    "0");
                yield return new TestCaseData(
                    new Trafficlight(
                        85,
                        new Tuple<bool[], bool[]>(
                            new[] { true, true, true, true, true, true, true },
                            new[] { true, true, false, true, false, true, true }),
                        Logger),
                    "83");
                yield return new TestCaseData(
                    new Trafficlight(
                        76,
                        new Tuple<bool[], bool[]>(
                            new[] { true, true, true, true, true, true, true },
                            new[] { true, true, false, true, false, true, true }),
                        Logger),
                    "59");
                yield return new TestCaseData(
                    new Trafficlight(
                       66,
                        new Tuple<bool[], bool[]>(
                            new[] { true, true, false, true, false, true, true },
                            new[] { true, true, false, true, false, true, true }),
                        Logger),
                    "49");
                yield return new TestCaseData(
                    new Trafficlight(
                       97,
                        new Tuple<bool[], bool[]>(
                            new[] { true, false, true, true, true, true, false },
                            new[] { true, false, true, true, true, true, false }),
                        Logger),
                    "89");
                yield return new TestCaseData(
                    new Trafficlight(
                       99,
                        new Tuple<bool[], bool[]>(
                            new[] { false, false, true, false, false, false, false },
                            new[] { false, false, true, false, false, false, false }),
                        Logger),
                    "49");
                yield return new TestCaseData(
                    new Trafficlight(
                       99,
                        new Tuple<bool[], bool[]>(
                            new[] { false, true, false, false, false, false, false },
                            new[] { false, true, false, false, false, false, false }),
                        Logger),
                    "69");
            }
        }
    }
}
