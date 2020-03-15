using Mondop.Templates.Model;

namespace Mondop.Templates.Parsers
{
    public interface IKeywordParser
    {
        void Parse(string data, ref TemplateElementContainer parent);
    }
}
