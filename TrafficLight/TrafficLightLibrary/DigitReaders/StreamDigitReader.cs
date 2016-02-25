using System;
using System.Collections.Generic;
using System.IO;
using TrafficLight.Domain.Core.Core;
using TrafficLight.Domain.Core.Interfaces;

namespace TrafficLight.Domain.Core.DigitReaders
{
    public class StreamDigitReader : IDigitReader
    {
        private readonly int _rightAnswer;
        private int _step;
        private readonly StreamReader _streamReader;
        private readonly IMaskReader _maskReader;

        public StreamDigitReader(StreamReader streamReader, IMaskReader maskReader)
        {
            _streamReader = streamReader;
            _maskReader = maskReader;
            var rowAnswer = _streamReader.ReadLine();
            if (rowAnswer == null)
                throw new ArgumentException();
            _rightAnswer = int.Parse(rowAnswer);
            _step = 0;
        }

        public List<Digit> ReadDigits()
        {
            _step++;
            var result = new List<Digit>();
            var sep = new[] { ' ', ',', '.', ':', '\t' };
            var row = _streamReader.ReadLine();
            if (string.IsNullOrEmpty(row))
                return null;
            var digits = row.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            foreach (var digit in digits)
            {
                var mask = _maskReader.GetMask(digit);
                result.Add(new Digit(mask));
            }

            return result;
        }

        public int GetRightAnswer()
        {
            return _rightAnswer;
        }

        public int GetStep()
        {
            return _step;
        }

        public int GetLastNumber()
        {
            return _rightAnswer - _step + 1;
        }
    }
}
