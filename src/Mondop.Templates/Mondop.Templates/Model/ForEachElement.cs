namespace Mondop.Templates.Model
{
    public class ForEachElement : TemplateElementContainer
    {
        public ForEachElement(TemplateElement parent) : base(parent)
        {

        }

        public string Identifier { get; set; }
        public string SourceReference { get; set; }
    }
}
