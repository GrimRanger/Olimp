using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficLightLibrary;

namespace TrafficLightTest
{
    [TestClass]
    public class TrafficLightTests
    {
        [TestMethod]
        public void Test_CorrectTrafficLight_1()
        {
            var traffic = new CorrectTrafficLight(20);
            var problem = new Problem();
            problem.SolveTrafficLightProblem(traffic);

            Assert.AreEqual(19, traffic.GetAnswer);
            Assert.AreEqual(2, traffic.NumberOfGetNextCalls);
        }

        [TestMethod]
        public void Test_CorrectTrafficLight_2()
        {
            var traffic = new CorrectTrafficLight(86);
            var problem = new Problem();
            problem.SolveTrafficLightProblem(traffic);

            Assert.AreEqual(85, traffic.GetAnswer);
            Assert.AreEqual(2, traffic.NumberOfGetNextCalls);
        }

        [TestMethod]
        public void Test_IncorrectTrafficLight_1()
        {
            var traffic = new IncorrectTrafficLight(58, new[] { 1, 2 }, new[] { 0, 2, 3, 6 });
            var problem = new Problem();
            problem.SolveTrafficLightProblem(traffic);

            Assert.AreEqual(49, traffic.GetAnswer);
            Assert.AreEqual(10, traffic.NumberOfGetNextCalls);
        }

        [TestMethod]
        public void Test_IncorrectTrafficLight_2()
        {
            var traffic = new IncorrectTrafficLight(86, new int[] { }, new[] { 2, 4 });
            var problem = new Problem();
            problem.SolveTrafficLightProblem(traffic);

            Assert.AreEqual(84, traffic.GetAnswer);
            Assert.AreEqual(3, traffic.NumberOfGetNextCalls);
        }

        [TestMethod]
        public void Test_IncorrectTrafficLight_3()
        {
            var traffic = new IncorrectTrafficLight(86, new int[] { 3 }, new[] { 2, 4 });
            var problem = new Problem();
            problem.SolveTrafficLightProblem(traffic);

            Assert.AreEqual(79, traffic.GetAnswer);
            Assert.AreEqual(8, traffic.NumberOfGetNextCalls);
        }

        [TestMethod]
        public void Test_IncorrectTrafficLight_4()
        {
            var traffic = new IncorrectTrafficLight(25, new [] { 0, 1, 2, 3, 4, 5, 6 }, new[] { 0, 1, 2, 3, 4, 5, 6 });
            var problem = new Problem();
            problem.SolveTrafficLightProblem(traffic);

            Assert.AreEqual(0, traffic.GetAnswer);
            Assert.AreEqual(27, traffic.NumberOfGetNextCalls);
        }

        [TestMethod]
        public void Test_IncorrectTrafficLight_5()
        {
            var traffic = new IncorrectTrafficLight(55, new [] { 0, 1, 2, 3, 4, 6 }, new[] { 0, 2, 4, 6 });
            var problem = new Problem();
            problem.SolveTrafficLightProblem(traffic);

            Assert.AreEqual(29, traffic.GetAnswer);
            Assert.AreEqual(27, traffic.NumberOfGetNextCalls);
        }

        [TestMethod]
        public void Test_IncorrectTrafficLight_6()
        {
            var traffic = new IncorrectTrafficLight(79, new int[] { 1, 5, 6 }, new[] { 4, 6 });
            var problem = new Problem();
            problem.SolveTrafficLightProblem(traffic);

            Assert.AreEqual(69, traffic.GetAnswer);
            Assert.AreEqual(11, traffic.NumberOfGetNextCalls);
        }
    }
}
