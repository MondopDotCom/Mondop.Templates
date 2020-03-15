using Mondop.Guard;
using Mondop.Templates.Model;
using System;
using System.Collections.Generic;

namespace Mondop.Templates.Processing
{
    public class ProcessData
    {
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
                { typeof(InputElement), new InputElementProcessor() },
                { typeof(NameElement), new NameElementProcessor()},
                { typeof(LiteralText) , new LiteralTextProcessor() },
                { typeof(MemberReference), new MemberReferenceProcessor() }
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
            };
            processingData.InputPath.Add(template.Input.Alias, input);

            foreach (var element in template.Children)
            {
                var elementProcessor = GetElementProcessor(element);
                elementProcessor.Process(element, processingData);
            }

            return "";
        }

        private IElementProcessor GetElementProcessor(TemplateElement templateElement)
        {
            return _processors[templateElement.GetType()];
        }
    }
}
