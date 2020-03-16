using System.Text;

namespace Mondop.Templates.Processing
{
    public interface IOutputWriter
    {
        void Write(string text);
        void WriteLn();
    }

    public class OutputWriter : IOutputWriter
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();
               
        public void Write(string text)
        {
            _stringBuilder.Append(text);
        }

        public void WriteLn()
        {
            _stringBuilder.AppendLine();
        }

        public string Output => _stringBuilder.ToString();
    }
}
