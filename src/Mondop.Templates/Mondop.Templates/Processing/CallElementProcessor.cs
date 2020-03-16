using Mondop.Templates.Model;

namespace Mondop.Templates.Processing
{
    public class CallElementProcessor: ElementProcessor<CallElement>
    {
        protected override void _Process(CallElement element, ProcessData processData)
        {
            base._Process(element, processData);

            var inputObject = processData.InputPath[element.TypeReference];

            processData.TemplateEngine.Process(inputObject,processData);
        }
    }
}
