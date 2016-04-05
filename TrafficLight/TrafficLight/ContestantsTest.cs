using System.Collections.Generic;
using NUnit.Framework;
using TrafficLight.Domain.Core;
using TrafficLight.Domain.Core.Filters;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight
{
    public partial class TrafficLightTests
    {
        //static int[] numbers = { 1, 3, 5, 9, 10, 15, 19, 25, 42, 66, 77, 88, 99 };
        //static string[] noises =
        //{
        //    "0000000",
        //    "0000001", "0010000", "1000000",
        //    "0011000", "1001000", "0010001", "0010100", 
        //    "1001001", "0000111", "1000011", "1010001",
        //    "0110011", "1010011", "0101011", "0100111", "0111100", "1111000",
        //    "1010111", "1100111", "0111110", "1111010", "0011111", "1011101",
        //    "0111111", "1101111", "1110111", "1111011", "1111110"
        //};

        static int[] numbers = { 9, 10, 15, 19, 25, 42, 77, 88, 99 };
        static string[] noises =
        {
            //"0000000",
            //"0000001", "0010000",
            //"0011000", "1001000", 
            //"1001001", "0000111", "1000011",
            //"0110011", "1010011",  "0111100", "1111000",
            //"1010111",  "1111010", 
            //"0111111",  "1111110",
            "1111111"
        };

        public static ActionContainer<ITrafficLight>[] ContestantSolves =
        {
            //new ActionContainer<ITrafficLight>("Sharkunova", new Anna_Sharkunova.Anna_Sharkunova().SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("Fedorov", new Andrey_Fedorov.Andrey_Fedorov().SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("Alexeev", Sergey_Alexeev.Sergey_Alexeev.SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("Ionov", Peter_Ionov.Peter_Ionov.SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("Makeyev", Andrei_Makeyev.TrafficLightProcessor.GetCurrentState),
            //new ActionContainer<ITrafficLight>("Kozlova", new Anna_Kozlova.Problem().SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("Kalachev", KarachevProject.Dmitry_Kalachev.Method),
            //new ActionContainer<ITrafficLight>("Koroleva", Irina_Koroleva.Irina_Koroleva.SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("Golubtsov", new Vasily_Golubtsov.StateDetector().SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("Belov", new Alexander_Belov.TrafficLightProblemSolver().SolveTrafficLightProblem),
            new ActionContainer<ITrafficLight>("Kuznetsov", new Artem_Kuznetsov.TrafficLightProblem().Solve),
            //new ActionContainer<ITrafficLight>("Galaida", Traffic2.Anna_Galaida.SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("Banokin", Banokin_KB21_SolveTrafficLightProblem.Banokin_KB21.SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("IlyashenkoK", IlyashenkoKB21.IlyashenkoKB21.SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("Sudarkin", new Trafficlight_Sudarkin.Trafficlight_Sudarkin().SolveTrafficLightProblem),
            //new ActionContainer<ITrafficLight>("Volyakov", Volyakov.Volyakov.TrafficlightProblem),
            //new ActionContainer<ITrafficLight>("Zhuchenko", Zhuchenko_Kristina.Zhuchenko_Kristina.SolveTrafficLightProblem),
            ////new ActionContainer<ITrafficLight>("Efimov",new Alexander_Efimov.Alexander_Efimov()),
            //new ActionContainer<ITrafficLight>("Mironych", Alexander_Mironych.StatusDeterminer.Determine),
            //new ActionContainer<ITrafficLight>("Nikitin", Алексей_Никитин.Alexei_Nikitin.Slove)
        };


        public static IEnumerable<object> TestData
        {
            get
            {
                foreach (var number in numbers)
                {
                    foreach (var firstNumberNoise in noises)
                    {
                        foreach (var secondNumberNoise in noises)
                        {
                            var testNoises = new[] { firstNumberNoise, secondNumberNoise };

                            foreach (var contestantSolve in ContestantSolves)
                            {
                                yield return new object[] { contestantSolve, number, testNoises };
                            }
                        }
                    }
                }
            }
        }

        [TestCaseSource("TestData")]
        public void TrafficLightAnalyzer_Analyze_TestAllContestantsWithExpected(ActionContainer<ITrafficLight> solveTrafficProblem, int number, string[] noises)
        {
            var filters = new List<INumberFilter> { new SequenceDigitFilter(), new MaskDigitFilter() };
            var digitAnalyzer = new TrafficLightAnalyzer(filters);
            var expectedResult = TrafficLightAnalyzer_Test(digitAnalyzer.Analyze, number, noises);
            Assert.AreEqual(expectedResult.RightAnswer, expectedResult.UserAnswer);

            var actualResult = TrafficLightAnalyzer_Test(solveTrafficProblem.Action, number, noises);
            TestContext.WriteLine(string.Format("Name : {0}", solveTrafficProblem.GetName));
            TestContext.WriteLine(string.Format("expected result : {0}", expectedResult.RightAnswer));
            TestContext.WriteLine(string.Format("actual result : {0}", actualResult.UserAnswer));
            TestContext.WriteLine(string.Format("expected steps : {0}", expectedResult.Steps));
            TestContext.WriteLine(string.Format("actual steps : {0}", actualResult.Steps));
            Assert.AreEqual(expectedResult.RightAnswer, actualResult.UserAnswer);
            Assert.AreEqual(expectedResult.Steps, actualResult.Steps);
        }


        [TestCaseSource("TestData")]
        public void TrafficLightAnalyzer_Analyze_TestAllContestantsWithoutExpected(ActionContainer<ITrafficLight> solveTrafficProblem, int number, string[] noises)
        {
            var actualResult = TrafficLightAnalyzer_Test(solveTrafficProblem.Action, number, noises);
            TestContext.WriteLine(string.Format("Name : {0}", solveTrafficProblem.GetName));
            TestContext.WriteLine(string.Format("expected result : {0}", actualResult.RightAnswer));
            TestContext.WriteLine(string.Format("actual result : {0}", actualResult.UserAnswer));
            TestContext.WriteLine(string.Format("actual steps : {0}", actualResult.Steps));
            Assert.AreEqual(actualResult.RightAnswer, actualResult.UserAnswer);
        }
    }
}

