using Mondop.Guard;
using Mondop.Templates.Model;
using System;
using System.Collections.Generic;

namespace Mondop.Templates.Processing
{
    public class TemplateEngine
    {
        private readonly ITemplateFactory _templateFactory;

        private readonly Dictionary<Type, IElementProcessor> _processors;
        private readonly IOutputResolver _outputResolver;

        public TemplateEngine(ITemplateFactory templateFactory, IOutputResolver outputResolver)
        {
            _templateFactory = Ensure.IsNotNull(templateFactory, nameof(templateFactory));
            _outputResolver = Ensure.IsNotNull(outputResolver, nameof(outputResolver));
            _processors = new Dictionary<Type, IElementProcessor>
            {
                { typeof(BreakElement), new BreakElementProcessor() },
                { typeof(InputElement), new InputElementProcessor() },
                { typeof(NameElement), new NameElementProcessor()},
                { typeof(LiteralText) , new LiteralTextProcessor() },
                { typeof(MemberReference), new MemberReferenceProcessor() },
                { typeof(ForEachElement), new ForeachElementProcessor() },
                {typeof(CallElement), new CallElementProcessor() },
            };
        }

        public IOutputWriter Process(object input)
        {
            var template = _templateFactory.GetTemplate(input);
            if (template == null)
                throw new InvalidOperationException($"Unable to resolve template for object {input.ToString()}");

            var outputWriter = _outputResolver.GetWriter(template, input);

            Process(template, input, outputWriter);

            return outputWriter;
        }

        internal void Process(object input, ProcessData processData)
        {
            var template = _templateFactory.GetTemplate(input);
            if (template == null)
                throw new InvalidOperationException($"Unable to resolve template for object {input.ToString()}");

            bool inputPathAdded = false;
            if (!processData.InputPath.ContainsKey(template.Input.Alias))
            {
                processData.InputPath.Add(template.Input.Alias, input);
                inputPathAdded = true;
            }

            ProcessChildren(template.Children, processData);

            if (inputPathAdded)
                processData.InputPath.Remove(template.Input.Alias);
        }

        private void Process(Template template, object input, IOutputWriter outputWriter)
        {
            var processingData = new ProcessData
            {
                OutputWriter = outputWriter,
                TemplateEngine = this
            };
            processingData.InputPath.Add(template.Input.Alias, input);
            ProcessChildren(template.Children, processingData);
        }

        internal void ProcessChildren(IEnumerable<TemplateElement> children, ProcessData processData)
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
