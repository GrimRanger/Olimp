using System;
using System.Collections.Generic;
using MultitonLibrary;
using NUnit.Framework;

namespace MultitonTests
{
    public class MultitonTest
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(17)]
        [TestCase(100)]
        public void Multiton_CreateNewInstances_CapacityAndCountAreEqual_ResultShouldBeTrue(int instanceCount)
        {
            Multiton.InstanceCapacity = instanceCount;
            var multitonList = new List<Multiton>();

            for (var i = 0; i < instanceCount; ++i)
            {
                multitonList.Add(new Multiton());
            }

            Assert.AreEqual(instanceCount, Multiton.InstanceCount);

            foreach (var multiton in multitonList)
            {
                multiton.Dispose();
            }
        }


        public void Multiton_CreateNewInstances_ExceptionInvoker(List<Multiton> multitonList, int instanceCount)
        {
            for (var i = 0; i <= instanceCount; ++i)
            {
                multitonList.Add(new Multiton());
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(17)]
        [TestCase(100)]
        public void Multiton_CreateNewInstances_CapacityLessThanCount_ResultShouldBeError(int instanceCount)
        {
            Multiton.InstanceCapacity = instanceCount;
            var multitonList = new List<Multiton>();
            Assert.Throws<IndexOutOfRangeException>(
                () => { Multiton_CreateNewInstances_ExceptionInvoker(multitonList, instanceCount); });

            foreach (var multiton in multitonList)
            {
                multiton.Dispose();
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(17)]
        [TestCase(100)]
        public void Multiton_DisposeInstances_CapacityBiggerThanCount_ResultShouldBeTrue(int instanceCount)
        {
            Multiton.InstanceCapacity = instanceCount;

            for (var j = 0; j < 10; ++j)
            {
                var multitonList = new List<Multiton>();
                for (var i = 0; i < instanceCount; ++i)
                {
                    multitonList.Add(new Multiton());
                }

                Assert.AreEqual(instanceCount, Multiton.InstanceCount);

                foreach (var multiton in multitonList)
                {
                    multiton.Dispose();
                }
            }
        }
    }
}
