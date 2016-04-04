using System;

namespace TrafficLightTask
{
	public interface ITrafficLight
	{
		Tuple<bool[], bool[]> Current { get; }
		bool GetNext();
		void Answer(int value);
	}
}