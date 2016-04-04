using System;
using System.Collections.Generic;
using TrafficLightTask;

namespace TrafficLightTaskTests.TestResources
{
	public class TestTrafficLight : ITrafficLight
	{
		private readonly Action<int> _answerAction;

		private readonly Tuple<bool[], bool[]>[] _data;

		private int _position;

		private TestTrafficLight(Action<int> answerAction, Tuple<bool[], bool[]>[] data)
		{
			_position = -1;
			_answerAction = answerAction;
			_data = data;
		}

		public Tuple<bool[], bool[]> Current => _data[_position];

		public bool GetNext()
		{
			if (_position + 1 < _data.Length)
			{
				_position++;
				return true;
			}

			return false;
		}

		public void Answer(int value)
		{
			_answerAction.Invoke(value);
		}

		public static TestTrafficLight Create(Action<int> answerAction, int startCount, int[] errorsBlocksNumbersFirstBlock,
			int[] errorsBlocksNumbersSecondBlock)
		{
			if (startCount > 99 || startCount < 1)
				throw new IndexOutOfRangeException();

			var tuplesWithData = new List<Tuple<bool[], bool[]>>();

			for (var currentDigit = startCount; currentDigit >= 0; currentDigit--)
			{
				var firstDigit = Convert.ToInt32(Math.Truncate(currentDigit/10D));
				var secondDigit = currentDigit - firstDigit*10;

				var boolFirstDigit = DigitsHelper.AvailableDigits[firstDigit];
				var boolSecondDigit = DigitsHelper.AvailableDigits[secondDigit];

				var firstWithErrors = DigitsHelper.MakeErrors(boolFirstDigit, errorsBlocksNumbersFirstBlock);
				var secondWithErrors = DigitsHelper.MakeErrors(boolSecondDigit, errorsBlocksNumbersSecondBlock);
				tuplesWithData.Add(new Tuple<bool[], bool[]>(firstWithErrors, secondWithErrors));
			}

			var data = tuplesWithData.ToArray();

			return new TestTrafficLight(answerAction, data);
		}
	}
}