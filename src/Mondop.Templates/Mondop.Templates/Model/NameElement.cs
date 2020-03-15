namespace Mondop.Templates.Model
{
    public class NameElement: TemplateElement
    {
        public NameElement(TemplateElement parent) : base(parent)
        {

        }

        public string Name { get; set; }
    }
}
