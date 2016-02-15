using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core
{
    public class TrafficLightAnalyzer
    {
        private readonly ITrafficLightService _trafficLightService;

        public TrafficLightAnalyzer(ITrafficLightService trafficLightService)
        {
            if (trafficLightService == null)
            {
                throw new ArgumentNullException("trafficLightService");
            }

            _trafficLightService = trafficLightService;
        }

        public void Analyze()
        {
            var possibleAnswers = new List<int>();

            var digits = _trafficLightService.GetNext();
            while (digits != null && digits.Item1 != null && digits.Item2 != null)
            {

                if (possibleAnswers.Count == 1)
                {
                    _trafficLightService.GiveAnswer(possibleAnswers[0]);
                }
                digits = _trafficLightService.GetNext();
            }
        }
    }
}
