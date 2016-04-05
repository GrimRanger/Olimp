using System;

namespace TrafficLight
{
    public class ActionContainer<T>
    {
        private readonly string _name;
        private readonly Action<T> _action;

        public ActionContainer(string name, Action<T> action)
        {
            _name = name;
            _action = action;
        }

        public void Action(T arg)
        {
            _action(arg);
        }

        public string GetName { get { return _name; } }
    }
}
