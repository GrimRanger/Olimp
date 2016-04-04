using System;
using System.Collections.Generic;
using System.Linq;
using TrafficLight.Domain.Core.Interfaces;

namespace Andrei_Makeyev
{
	public static class TrafficLightProcessor
	{
		public static void GetCurrentState(ITrafficLight trafficLight)
		{
			var firstDigitPossibilitiesHistory = new List<Dictionary<int, double>>();
			var secondDigitPossibilitiesHistory = new List<Dictionary<int, double>>();

			var firstGuess = 0;

			var secondGuess = 0;
			var secondFound = false;

			var firstBlockWorkingCells = new bool[7];
			var secondBlockWorkingCells = new bool[7];

			while (trafficLight.GetNext())
			{
				if (secondFound)
				{
					secondGuess = secondGuess == 0 ? 9 : secondGuess - 1;
				}

				var firstDigit = TrafficLightDigit.Create(trafficLight.Current.Item1);
				var secondDigit = TrafficLightDigit.Create(trafficLight.Current.Item2);

				firstDigit.MarkWorkingCells(firstBlockWorkingCells);
				secondDigit.MarkWorkingCells(secondBlockWorkingCells);

				var firstPossibleDigit = GetPossibleForFirstDigit(firstDigit, firstDigitPossibilitiesHistory, firstBlockWorkingCells);
				bool firstFound;
				if (firstPossibleDigit > -1)
				{
					firstFound = true;
					firstGuess = firstPossibleDigit;
				}
				else
				{
					firstFound = false;
				}

				if (!secondFound)
				{
					var secondPossibleDigit = GetPossibleForSecondDigit(secondDigit, secondDigitPossibilitiesHistory,
						secondBlockWorkingCells);
					if (secondPossibleDigit > -1)
					{
						secondFound = true;
						secondGuess = secondPossibleDigit;
					}
				}

				if (firstFound && secondFound)
				{
					break;
				}
			}

			trafficLight.Answer(firstGuess*10 + secondGuess);
		}

		private static int GetPossibleForFirstDigit(TrafficLightDigit digit,
			List<Dictionary<int, double>> firstDigitPossibilitiesHistory, bool[] workingCells)
		{
			var posFirst = digit.GetPossibilities(workingCells);
			firstDigitPossibilitiesHistory.Add(posFirst);

			if (posFirst.ContainsKey(8) && Math.Abs(posFirst[8] - 100) <= 0)
			{
				return 8;
			}

			var variants = new List<Dictionary<int, double>>();

			foreach (var variant in firstDigitPossibilitiesHistory.Where(variant => !AlreadyExists(variants, variant)))
			{
				variants.Add(variant);
			}

			return GetPossibleVariantsForDigitBlock(variants);
		}

		private static int GetPossibleForSecondDigit(TrafficLightDigit digit,
			List<Dictionary<int, double>> digitPossibilitiesHistory, bool[] workingCells)
		{
			var possibilities = digit.GetPossibilities(workingCells);

			if (possibilities.ContainsKey(8) && Math.Abs(possibilities[8] - 100) <= 0)
			{
				return 8;
			}

			digitPossibilitiesHistory.Add(possibilities);

			return GetPossibleVariantsForDigitBlock(digitPossibilitiesHistory);
		}

		private static int GetPossibleVariantsForDigitBlock(IReadOnlyList<Dictionary<int, double>> digitPossibilitiesHistory)
		{
			var possibleDigit = PredictPossibleDigit(digitPossibilitiesHistory);
			if (possibleDigit > -1)
			{
				return possibleDigit;
			}

			return -1;
		}

		private static int PredictPossibleDigit(IReadOnlyList<Dictionary<int, double>> digitPossibilitiesHistory)
		{
			var correctVariants = new List<int>();

			for (var i = 0; i < digitPossibilitiesHistory.Count - 1; i++)
			{
				correctVariants = correctVariants.Select(val => val - 1).Where(val => val >= 0).ToList();

				var second = digitPossibilitiesHistory[i];

				for (var j = i + 1; j < digitPossibilitiesHistory.Count; j++)
				{
					var secondCompare = digitPossibilitiesHistory[j];
					var tempVariants = new List<int>();
					foreach (var item1 in second)
					{
						foreach (var item2 in secondCompare)
						{
							var diff = item1.Key - item2.Key;
							if (diff == 1)
							{
								if (!tempVariants.Contains(item2.Key))
									tempVariants.Add(item2.Key);
							}
						}
					}

					if (correctVariants.Count == 0)
					{
						foreach (var variant in tempVariants)
						{
							if (!correctVariants.Contains(variant))
								correctVariants.Add(variant);
						}
					}

					correctVariants = correctVariants.Where(val => tempVariants.Contains(val)).ToList();
				}
			}

			if (correctVariants.Count != 1)
				return -1;

			return correctVariants[0];
		}

		private static bool AlreadyExists(IEnumerable<Dictionary<int, double>> items,
			IReadOnlyDictionary<int, double> itemToSearch)
		{
			var exists = false;
			foreach (var item in items)
			{
				if (item.Count != itemToSearch.Count)
					continue;

				foreach (var d in item)
				{
					if (!itemToSearch.ContainsKey(d.Key) || !(Math.Abs(itemToSearch[d.Key] - d.Value) <= 0))
					{
						break;
					}
				}
				exists = true;
				break;
			}

			return exists;
		}
	}
}