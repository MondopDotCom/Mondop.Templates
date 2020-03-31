namespace Mondop.Templates.Processing
{
    public interface IOutputWriter
    {
        void Write(string text);
        void WriteLn();

        string Output { get; }
    }
}
