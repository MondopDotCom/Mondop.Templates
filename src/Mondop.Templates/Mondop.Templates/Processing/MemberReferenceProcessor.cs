using Mondop.Templates.Model;
using System;

namespace Mondop.Templates.Processing
{
    internal class MemberReferenceProcessor: ElementProcessor<MemberReference>
    {
        protected override void _Process(MemberReference element, ProcessData processData)
        {
            base._Process(element, processData);

            var parts = element.Member.Split('.');

            if (parts.Length < 2)
                throw new InvalidOperationException("Member reference should have at least 2 parts");

            var targetName = parts[0];
            var targetObject = processData.InputPath[targetName];

            for(int i=1;i<parts.Length;i++)
            {
                var property = targetObject.GetType().GetProperty(parts[1]);

                targetObject = property.GetValue(targetObject);
            }

            processData.OutputWriter.Write(targetObject.ToString());
        }
    }
}
