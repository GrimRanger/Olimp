using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace KarachevProject
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        //Выполнил Карачёв Дмитрий
        public static void Method(ITrafficLight it)
        {
            var isNotZero = it.GetNext();
            var current = it.Current;
            if (!isNotZero)
            {
                it.Answer(0);
                return;
            }

            var helper = new TrafficLightHelper();
            int number;
            var isOver = helper.UpdateCandidates(current, out number);
            while (!isOver)
            {
                isNotZero = it.GetNext();
                current = it.Current;
                if (!isNotZero)
                {
                    it.Answer(0);
                    return;
                }
                isOver = helper.UpdateCandidates(current, out number);
            }
            it.Answer(number);
        }

    }
}

