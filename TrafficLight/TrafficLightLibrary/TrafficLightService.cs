using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core
{
    public class TrafficLightService : ITrafficLightService
    {
        private readonly IDigitReader _digitReader;


        public TrafficLightService(IDigitReader digitReader)
        {
            _digitReader = digitReader;
        }

        public List<Digit> GetNext()
        {
            return _digitReader.ReadDigits();
        }

        public bool GiveAnswer(int answer)
        {
            return _digitReader.GetRightAnsert() == answer;
        }
    }
}
