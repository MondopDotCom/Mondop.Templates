namespace Mondop.Templates.Model
{
    public class CallElement : TemplateElement
    {
        public CallElement(TemplateElement parent) : base(parent)
        {

        }

        public string TypeReference { get; set; }
    }
}
