using System;
using TrafficLightTask;
using TrafficLightTaskTests.TestResources;

namespace ConfirmitTestApp
{
	internal static class Program
	{
		private static void Main()
		{
			TrafficLightProcessor.GetCurrentState(
				TestTrafficLight.Create(
					Console.WriteLine,
					21,
					null,
					new[] {2, 4}
					)
				);

			Console.ReadKey();
		}
	}
}