using Mondop.Templates.Model;

namespace Mondop.Templates.Parsers
{
    public class MemberReferenceParser : IKeywordParser
    {
        public void Parse(string data, ref TemplateElementContainer parent)
        {
            var newMemberReference = new MemberReference(parent)
            {
                Member = data
            };
            parent.Children.Add(newMemberReference);
        }
    }
}
