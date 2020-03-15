using Mondop.Templates.Model;
using System;
using System.Text.RegularExpressions;

namespace Mondop.Templates.Parsers
{
    public class InputParser : IKeywordParser
    {
        public void Parse(string data, ref TemplateElementContainer parent)
        {
            var regex = @"input\s(?<type>.*)\sas\s(?<alias>.*)";

            var match = Regex.Match(data, regex);
            if (!match.Success)
                throw new InvalidOperationException($"Unable to parse input epxression: {data}");

            var newInput = new InputElement(parent)
            {
                Type = match.Groups["type"].Value,
                Alias = match.Groups["alias"].Value
            };
            parent.Children.Add(newInput);
        }
    }
}
