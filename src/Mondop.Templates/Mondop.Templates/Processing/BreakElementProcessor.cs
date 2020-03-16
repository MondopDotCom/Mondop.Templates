using Mondop.Templates.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mondop.Templates.Processing
{
    public class BreakElementProcessor: ElementProcessor<BreakElement>
    {
        protected override void _Process(BreakElement element, ProcessData processData)
        {
            base._Process(element, processData);

            processData.OutputWriter.WriteLn();
        }
    }
}
