using Mondop.Templates.Model;

namespace Mondop.Templates.Parsers
{
    public class EndParser : IKeywordParser
    {
        public void Parse(string data, ref TemplateElementContainer parent)
        {
            parent = parent?.Parent as TemplateElementContainer;
        }
    }
}
