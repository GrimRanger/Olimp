using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vasily_Golubtsov
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> breakBitsLeft = new List<int>();
            List<int> breakBitsRight = new List<int>();
            breakBitsRight.Add(2);
            breakBitsRight.Add(4);
            Trafficlight tL = new Trafficlight(86, breakBitsLeft, breakBitsRight);
            StateDetector sD = new StateDetector();
            sD.SolveTrafficLightProblem(tL);
        }
    }
}
