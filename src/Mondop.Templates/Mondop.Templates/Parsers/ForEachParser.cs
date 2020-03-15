using Mondop.Templates.Model;
using System;
using System.Text.RegularExpressions;

namespace Mondop.Templates.Parsers
{
    public class ForEachParser : IKeywordParser
    {
        public void Parse(string data, ref TemplateElementContainer parent)
        {
            var regex = @"foreach\(\s*(?<identifier>.*)\s+in\s+(?<source>.*)\s*\)";

            var match = Regex.Match(data, regex);
            if (!match.Success)
                throw new InvalidOperationException($"Unable to parse foreach epxression: {data}");

            var newForeach = new ForEachElement(parent)
            {
                Identifier = match.Groups["identifier"].Value,
                SourceReference = match.Groups["source"].Value
            };
            parent.Children.Add(newForeach);

            parent = newForeach;
        }
    }
}
