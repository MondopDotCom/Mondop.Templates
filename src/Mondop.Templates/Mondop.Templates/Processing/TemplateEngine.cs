using Mondop.Guard;
using Mondop.Templates.Model;
using System;
using System.Collections.Generic;

namespace Mondop.Templates.Processing
{
    public class ProcessData
    {
        public TemplateEngine TemplateEngine { get; set; }
        public IOutputWriter OutputWriter { get; set; }
        public Dictionary<string, object> InputPath { get; } = new Dictionary<string, object>();
    }

    public class TemplateEngine
    {
        private readonly TemplateFactory _templateFactory;

        private readonly Dictionary<Type, IElementProcessor> _processors;

        public TemplateEngine(TemplateFactory templateFactory)
        {
            _templateFactory = Ensure.IsNotNull(templateFactory, nameof(templateFactory));
            _processors = new Dictionary<Type, IElementProcessor>
            {
                { typeof(BreakElement), new BreakElementProcessor() },
                { typeof(InputElement), new InputElementProcessor() },
                { typeof(NameElement), new NameElementProcessor()},
                { typeof(LiteralText) , new LiteralTextProcessor() },
                { typeof(MemberReference), new MemberReferenceProcessor() },
                { typeof(ForEachElement), new ForeachElementProcessor() }
            };
        }

        public object Process(object input, IOutputWriter outputWriter)
        {
            var template = _templateFactory.GetTemplate(input);
            if (template == null)
                throw new InvalidOperationException($"Unable to resolve template for object {input.ToString()}");

            return Process(template, input, outputWriter);
        }

        public object Process(Template template, object input, IOutputWriter outputWriter)
        {
            var processingData = new ProcessData
            {
                OutputWriter = outputWriter,
                TemplateEngine = this
            };
            processingData.InputPath.Add(template.Input.Alias, input);
            ProcessChildren(template.Children, processingData);

            return "";
        }

        public void ProcessChildren(IEnumerable<TemplateElement> children,ProcessData processData)
        {
            foreach (var element in children)
            {
                var elementProcessor = GetElementProcessor(element);
                elementProcessor.Process(element, processData);
            }
        }

        private IElementProcessor GetElementProcessor(TemplateElement templateElement)
        {
            return _processors[templateElement.GetType()];
        }
    }
}
