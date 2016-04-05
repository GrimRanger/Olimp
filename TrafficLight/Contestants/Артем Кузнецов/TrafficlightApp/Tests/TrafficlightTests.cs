using System;
using System.Collections;
using NUnit.Framework;
using TrafficlightApp;

namespace Tests
{
    [TestFixture]
    public class TrafficlightTests
    {
        [Test, TestCaseSource("TestCases")]
        public void CommonTest(int start, Tuple<bool[], bool[]> workingCells, int expected)
        {
            var trafficlight = new Trafficlight(start, workingCells, new Logger());

            var i = 0;
            while (trafficlight.GetNext())
                i++;

            Assert.AreEqual(expected, i);
        }

        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(
                    0,
                    new Tuple<bool[], bool[]>(
                        new[] { true, true, true, true, true, true, true },
                        new[] { true, true, true, true, true, true, true }),
                    0);
                yield return new TestCaseData(
                    1,
                    new Tuple<bool[], bool[]>(
                        new[] { true, true, true, true, true, true, true },
                        new[] { true, true, true, true, true, true, true }),
                    1);
                yield return new TestCaseData(
                    15,
                    new Tuple<bool[], bool[]>(
                        new[] { true, true, true, true, true, true, true },
                        new[] { true, true, true, true, true, true, true }),
                    15);
            }
        }

        [Test, TestCaseSource("TestCasesInvalidData")]
        public void InvalidData(int start, Tuple<bool[], bool[]> workingCells)
        {
            var logger = new Logger();

            Assert.Throws(typeof(ArgumentException), () => new Trafficlight(start, workingCells, logger));
        }

        public static IEnumerable TestCasesInvalidData
        {
            get
            {
                yield return new TestCaseData(
                    -1,
                    new Tuple<bool[], bool[]>(
                        new[] { true, true, true, true, true, true, true },
                        new[] { true, true, true, true, true, true, true }));
                yield return new TestCaseData(
                    100,
                    new Tuple<bool[], bool[]>(
                        new[] { true, true, true, true, true, true, true },
                        new[] { true, true, true, true, true, true, true }));
                yield return new TestCaseData(
                    10,
                    new Tuple<bool[], bool[]>(
                        new[] { true },
                        new[] { true, true, true, true, true, true, true }));
                yield return new TestCaseData(
                    10,
                    null);
            }
        }
    }
}
