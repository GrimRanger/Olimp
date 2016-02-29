using System;
using System.Collections.Generic;
using System.Text;
using TrafficLight.Domain.Core.Core;

namespace TrafficLightHelper
{
    public class TrafficLightPainter
    {
       
        private string PrintVerticalLine(List<Digit> digits, int leftMask, int rightMask, char line)
        {
            var result = new StringBuilder("");
            foreach (var digit in digits)
            {
                if ((digit.Mask & leftMask) == leftMask)
                    result.Append(string.Format("{0} ", line));
                else
                {
                    result.Append("  ");
                }
                if ((digit.Mask & rightMask) == rightMask)
                    result.Append(string.Format("{0} ", line));
                else
                {
                    result.Append("  ");
                }
            }

            return result.ToString();
        }

        private string PrintHorizontalLine(List<Digit> digits, int mask, char line)
        {
            var result = new StringBuilder("");
            foreach (var digit in digits)
            {
                if ((digit.Mask & mask) == mask)
                    result.Append(string.Format(" {0}  ", line));
                else
                {
                    result.Append("    ");
                }
            }

            return result.ToString();
        }

        public string PrintNumber(List<Digit> digits)
        {
            var result = "";
            result += PrintHorizontalLine(digits, 64, '-');
            result += "\n";
            result += PrintVerticalLine(digits, 32, 16, '|');
            result += "\n";
            result += PrintHorizontalLine(digits, 8, '-');
            result += "\n";
            result += PrintVerticalLine(digits, 4, 2, '|');
            result += "\n";
            result += PrintHorizontalLine(digits, 1, '-');
            result += "\n";

            return result;
        }
    }
}
