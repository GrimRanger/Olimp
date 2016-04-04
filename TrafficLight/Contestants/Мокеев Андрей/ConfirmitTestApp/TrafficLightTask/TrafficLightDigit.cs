using System.Collections.Generic;
using System.Linq;

namespace Andrei_Makeyev
{
	public class TrafficLightDigit
	{
		private static readonly Dictionary<int, bool[]> AvailableDigits = new Dictionary<int, bool[]>
		{
			{8, new[] {true, true, true, true, true, true, true}},
			{9, new[] {true, true, true, true, false, true, true}},
			{0, new[] {true, true, true, false, true, true, true}},
			{6, new[] {true, true, false, true, true, true, true}},
			{5, new[] {true, true, false, true, false, true, true}},
			{2, new[] {true, false, true, true, true, false, true}},
			{3, new[] {true, false, true, true, false, true, true}},
			{7, new[] {true, false, true, false, false, true, false}},
			{4, new[] {false, true, true, true, false, true, false}},
			{1, new[] {false, false, true, false, false, true, false}}
		};

		private readonly bool[] _cells;

		private TrafficLightDigit(bool[] cells)
		{
			_cells = cells;
		}

		public Dictionary<int, double> GetPossibilities(bool[] workingCells)
		{
			var possibilities = new Dictionary<int, double>();

			foreach (var digit in AvailableDigits)
			{
				if (CanBeThisDigit(digit.Value, workingCells))
				{
					var dist = CalculateDistance(digit.Value);
					var percent = dist > 0 ? 100D*(7 - dist)/7D : 100D;

					possibilities.Add(digit.Key, percent);
				}
			}

			var result =
				possibilities
					.OrderByDescending(pair => pair.Value)
					.ToDictionary(pair => pair.Key, pair => pair.Value);

			return result;
		}

		public bool CanBeThisDigit(bool[] vals, bool[] workingCells)
		{
			for (var i = 0; i < _cells.Length; i++)
			{
				if (_cells[i] && vals[i] != true)
					return false;
				if (workingCells[i] && !_cells[i] && vals[i])
					return false;
			}
			return true;
		}

		private int CalculateDistance(bool[] vals)
		{
			return _cells.Where((cell, i) => cell != vals[i]).Count();
		}


		public static TrafficLightDigit Create(bool[] cells)
		{
			if (cells.Length < 7)
			{
				var extendedCells = cells.ToList();
				for (var i = 0; i < 7 - cells.Length; i++)
				{
					extendedCells.Add(false);
				}

				cells = extendedCells.ToArray();
			}

			if (cells.Length > 7)
				cells = cells.Take(7).ToArray();

			return new TrafficLightDigit(cells);
		}

		public void MarkWorkingCells(bool[] cells)
		{
			for (var i = 0; i < _cells.Length; i++)
			{
				if (_cells[i])
					cells[i] = true;
			}
		}
	}
}