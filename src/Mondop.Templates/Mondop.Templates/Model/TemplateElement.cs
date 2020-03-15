namespace Mondop.Templates.Model
{
    public class TemplateElement
    {
        public TemplateElement(TemplateElement parent)
        {
            Parent = parent;
        }

        public virtual T OfType<T>() where T: TemplateElement
        {
            if (this.GetType() == typeof(T))
                return (T)this;

            return null;
        }
        public TemplateElement Parent { get; set; }
    }
}
