using Mondop.Templates.Model;

namespace Mondop.Templates.Processing
{
    public interface IElementProcessor
    {
        void Process(TemplateElement element, ProcessData processData);
    }
    public class ElementProcessor<T> : IElementProcessor where T : TemplateElement
    {
        public void Process(TemplateElement element, ProcessData processData) =>
            _Process((T)element, processData);

        protected virtual void _Process(T element, ProcessData processData)
        {

        }
    }
}
