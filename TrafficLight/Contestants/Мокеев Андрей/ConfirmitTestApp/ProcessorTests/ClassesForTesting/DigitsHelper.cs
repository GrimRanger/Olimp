using System.Collections.Generic;
using System.Linq;

namespace TrafficLightTaskTests.TestResources
{
	public static class DigitsHelper
	{
		public static readonly Dictionary<int, bool[]> AvailableDigits = new Dictionary<int, bool[]>
		{
			{0, new[] {true, true, true, false, true, true, true}},
			{1, new[] {false, false, true, false, false, true, false}},
			{2, new[] {true, false, true, true, true, false, true}},
			{3, new[] {true, false, true, true, false, true, true}},
			{4, new[] {false, true, true, true, false, true, false}},
			{5, new[] {true, true, false, true, false, true, true}},
			{6, new[] {true, true, false, true, true, true, true}},
			{7, new[] {true, false, true, false, false, true, false}},
			{8, new[] {true, true, true, true, true, true, true}},
			{9, new[] {true, true, true, true, false, true, true}}
		};

		public static bool[] MakeErrors(bool[] data, int[] errorsBlocksNumbers)
		{
			if (errorsBlocksNumbers == null || errorsBlocksNumbers.Length == 0)
				return data;

			var digit = (bool[]) data.Clone();
			for (var i = 0; i < data.Length; i++)
			{
				if (errorsBlocksNumbers.Contains(i))
					digit[i] = false;
			}

			return digit;
		}
	}
}