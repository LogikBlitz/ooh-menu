using System.Text;

namespace ConsoleMenuSystem.StringHelpers
{
    public class StringBuilderExtended
    {
        private readonly StringBuilder _builder;

        public StringBuilder StringBuilder
        {
            get { return _builder; }
        }

        public StringBuilderExtended() : this(new StringBuilder()) {}

        public StringBuilderExtended(int capacity) : this(new StringBuilder(capacity)) {}

        public StringBuilderExtended(StringBuilder builder)
        {
            _builder = builder;
            Clear();
        }

        public void Clear()
        {
            LineCount = 0;
            StringBuilder.Clear();
        }

        public void AppendLine(string data)
        {
            StringBuilder.AppendLine(data);
            LineCount++;
        }

        public void Append(string data)
        {
            LineCount += data.Split('\n').Length;
            StringBuilder.Append(data);
        }

        public int LineCount { get; private set; }

        public override string ToString()
        {
            return StringBuilder.ToString();
        }
    }
}