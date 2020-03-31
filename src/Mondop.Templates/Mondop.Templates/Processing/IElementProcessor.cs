using Mondop.Templates.Model;

namespace Mondop.Templates.Processing
{
    internal interface IElementProcessor
    {
        void Process(TemplateElement element, ProcessData processData);
    }
}
