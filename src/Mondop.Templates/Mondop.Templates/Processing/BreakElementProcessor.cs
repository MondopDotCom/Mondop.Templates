using Mondop.Templates.Model;

namespace Mondop.Templates.Processing
{
    internal class BreakElementProcessor: ElementProcessor<BreakElement>
    {
        protected override void _Process(BreakElement element, ProcessData processData)
        {
            base._Process(element, processData);

            processData.OutputWriter.WriteLn();
        }
    }
}
