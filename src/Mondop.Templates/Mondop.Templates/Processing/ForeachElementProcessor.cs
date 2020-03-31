using Mondop.Templates.Model;
using System;
using System.Collections;

namespace Mondop.Templates.Processing
{
    internal class ForeachElementProcessor : ElementProcessor<ForEachElement>
    {
        protected override void _Process(ForEachElement element, ProcessData processData)
        {
            base._Process(element, processData);

            processData.InputPath.Add(element.Identifier, null);

            var enumerator = GetEnumerator(element.SourceReference, processData);
            while (enumerator.MoveNext())
            {
                processData.InputPath[element.Identifier] = enumerator.Current;
                processData.TemplateEngine.ProcessChildren(element.Children, processData);
            }
            processData.InputPath.Remove(element.Identifier);
        }

        private IEnumerator GetEnumerator(string sourceReference, ProcessData processData)
        {
            var parts = sourceReference.Split('.');

            if (parts.Length < 2)
                throw new InvalidOperationException("Member reference should have at least 2 parts");

            var targetName = parts[0];
            var targetObject = processData.InputPath[targetName];

            for (int i = 1; i < parts.Length; i++)
            {
                var property = targetObject.GetType().GetProperty(parts[1]);
                targetObject = property.GetValue(targetObject);
            }

            if (targetObject is IEnumerable enumerableObject)
                return enumerableObject.GetEnumerator();

            throw new InvalidOperationException($"Foreach source {sourceReference} is not enumerable");
        }
    }
}
