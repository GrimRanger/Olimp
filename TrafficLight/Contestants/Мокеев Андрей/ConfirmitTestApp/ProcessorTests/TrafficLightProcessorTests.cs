using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficLightTask;
using TrafficLightTaskTests.TestResources;

namespace TrafficLightTaskTests
{
	[TestClass]
	public class TrafficLightProcessorTests
	{
		[TestMethod]
		public void SuccessProcessTest()
		{
			TrafficLightProcessor.GetCurrentState(
				TestTrafficLight.Create(
					value => Assert.AreEqual(0, value),
					6,
					null,
					new[] {2, 4}
					)
				);
		}

		[TestMethod]
		public void SuccessProcessTest_MoreHard_SameBlocksFailed()
		{
			TrafficLightProcessor.GetCurrentState(
				TestTrafficLight.Create(
					value => Assert.AreEqual(0, value),
					9,
					null,
					new[] {2, 4}
					)
				);
		}

		[TestMethod]
		public void SuccessProcessTest_MoreHard1()
		{
			TrafficLightProcessor.GetCurrentState(
				TestTrafficLight.Create(
					value => Assert.AreEqual(0, value),
					16,
					null,
					new[] {2, 4}
					)
				);
		}

		[TestMethod]
		public void SuccessProcessTest_MoreHard2()
		{
			TrafficLightProcessor.GetCurrentState(
				TestTrafficLight.Create(
					value => Assert.AreEqual(19, value),
					26,
					null,
					new[] {2, 4}
					)
				);
		}

		[TestMethod]
		public void SuccessProcessTest_MoreHard3()
		{
			TrafficLightProcessor.GetCurrentState(
				TestTrafficLight.Create(
					value => Assert.AreEqual(29, value),
					36,
					null,
					new[] {2, 4}
					)
				);
		}

		[TestMethod]
		public void SuccessProcessTest_MoreHard4()
		{
			TrafficLightProcessor.GetCurrentState(
				TestTrafficLight.Create(
					value => Assert.AreEqual(87, value),
					89,
					null,
					new[] {2, 4}
					)
				);
		}

		[TestMethod]
		public void SuccessProcessTest_Finding88()
		{
			TrafficLightProcessor.GetCurrentState(
				TestTrafficLight.Create(
					value => Assert.AreEqual(88, value),
					88,
					null,
					null
					)
				);
		}
	}
}