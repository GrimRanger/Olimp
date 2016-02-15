using System;

namespace MultitonLibrary
{
    public class Multiton : IDisposable
    {
        private static int _instanceCount = 0;

        private static int _instanceCapacity = 0;

        public static int InstanceCapacity
        {
            get { return _instanceCapacity; }

            set
            {
                if (_instanceCount == 0 && value >= 0)
                    _instanceCapacity = value;
            }
        }

        public static int InstanceCount
        {
            get { return _instanceCount; }
        }

        public int InstanceNumber { get; private set; }

        public Multiton()
        {
            if (_instanceCount >= _instanceCapacity)
                throw new IndexOutOfRangeException();

            _instanceCount++;
            InstanceNumber = _instanceCount;
        }

        ~Multiton()
        {
            _instanceCount--;
        }

        public void Dispose()
        {
            _instanceCount--;
        }
    }
}
