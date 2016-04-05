using System;
using System.Collections.Generic;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Helpers;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core
{
    public class TrafficLight : ITrafficLight
    {
        private readonly IDigitReader _digitReader;
        private readonly DigitHelper _digitHelper;

        private int UserAnswer { get;  set; }
        private int RightAnswer { get {return  _digitReader.GetRightAnswer(); } }
        private int Steps { get { return _digitReader.GetStep(); } }
        public Tuple<bool[], bool[]> Current { get; private set; }
        public List<Digit> CurrentDigits { get; private set; }

        public TrafficLight(IDigitReader digitReader)
        {
            _digitReader = digitReader;
            _digitHelper = new DigitHelper();
        }

        public void Answer(int value)
        {
            UserAnswer = value;
        }

        public bool GetNext()
        {
            if (_digitReader.IsFinalState())
                return false;
            var digits = _digitReader.ReadDigits();
            CurrentDigits = digits;

            Current = _digitHelper.ToBool(digits);
            return true;
        }

        public TrafficLightResult GetResult()
        {
            return new TrafficLightResult(UserAnswer, RightAnswer, Steps);
        }
    }
}
