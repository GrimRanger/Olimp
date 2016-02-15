using System;
using MultitonLibrary;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int instanceCount = 10;
            Multiton.InstanceCapacity = instanceCount;
            using (var firstInstance = new Multiton())
            {
                Console.WriteLine("Instance {0}", firstInstance.InstanceNumber);
            }

            for (var i = 0; i < instanceCount; ++i)
            {
                var instance = new Multiton();
                Console.WriteLine("Instance {0}", instance.InstanceNumber);
            }
        }
    }
}
