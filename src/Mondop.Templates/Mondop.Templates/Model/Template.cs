namespace Mondop.Templates.Model
{
    public class Template : TemplateElementContainer
    {
        public Template() : base(null)
        {

        }

        public InputElement Input => this.OfType<InputElement>();
        public string Name => this.OfType<NameElement>()?.Name??"";
    }
}
