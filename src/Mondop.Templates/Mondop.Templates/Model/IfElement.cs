namespace Mondop.Templates.Model
{
    public class IfElement : TemplateElement
    {
        public IfElement(TemplateElement parent) : base(parent)
        {

        }

        public ElseElement Else { get; set; }

    }
}
