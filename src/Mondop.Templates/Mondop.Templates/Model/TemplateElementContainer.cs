using System.Collections.Generic;
using System.Linq;

namespace Mondop.Templates.Model
{
    public class TemplateElementContainer : TemplateElement
    {
        public TemplateElementContainer(TemplateElement parent) : base(parent)
        {

        }

        public override T OfType<T>()
        {
            var element = base.OfType<T>();
            if (element != null)
                return element;

            foreach(var child in Children)
            {
                element = child.OfType<T>();
                if (element != null)
                    return element;
            }

            return null;
        }

        public List<TemplateElement> Children { get; set; } = new List<TemplateElement>();
    }
}
