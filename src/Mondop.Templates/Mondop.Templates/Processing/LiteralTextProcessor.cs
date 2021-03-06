﻿using Mondop.Templates.Model;

namespace Mondop.Templates.Processing
{
    internal class LiteralTextProcessor: ElementProcessor<LiteralText>
    {
        protected override void _Process(LiteralText element, ProcessData processData)
        {
            base._Process(element, processData);

            processData.OutputWriter.Write(element.Text);
        }
    }
}
