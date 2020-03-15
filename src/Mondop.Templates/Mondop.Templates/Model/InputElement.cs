using System;

namespace Mondop.Templates.Model
{
    public class InputElement : TemplateElement
    {
        public InputElement(TemplateElement parent) : base(parent)
        {

        }
        public string Type { get; set; }
        public string Alias { get; set; }

        public Type ToType()
        {
            return System.Type.GetType(Type);
        }
    }

}
