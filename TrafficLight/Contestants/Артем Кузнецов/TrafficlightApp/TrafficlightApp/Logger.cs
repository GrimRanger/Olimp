using System.Text;

namespace Artem_Kuznetsov
{
    public interface ILogger
    {
        void Write(string str);
        void WriteLine(string str);
        void Clear();
    }

    public class Logger : ILogger
    {
        private readonly StringBuilder _log;

        public Logger()
        {
            _log = new StringBuilder();
        }

        public void Write(string str)
        {
            _log.Append(str);
        }

        public void WriteLine(string str)
        {
            _log.Append(str + "\n");
        }

        public void Clear()
        {
            _log.Clear();
        }

        public override string ToString()
        {
            return _log.ToString();
        }
    }
}