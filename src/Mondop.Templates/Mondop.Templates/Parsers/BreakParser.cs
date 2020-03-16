using Mondop.Templates.Model;

namespace Mondop.Templates.Parsers
{
    public class BreakParser : IKeywordParser
    {
        public void Parse(string data, ref TemplateElementContainer parent)
        {
            parent.Children.Add(new BreakElement(parent));
        }
    }
}
