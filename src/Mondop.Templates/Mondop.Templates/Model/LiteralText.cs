namespace Mondop.Templates.Model
{
    public class LiteralText : TemplateElement
    {
        public LiteralText(TemplateElement parent) : base(parent)
        {

        }

        public string Text { get; set; }
    }
}
