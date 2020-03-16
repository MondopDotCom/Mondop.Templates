using Mondop.Templates.Model;
using System;
using System.Text.RegularExpressions;

namespace Mondop.Templates.Parsers
{
    public class CallParser : IKeywordParser
    {
        public void Parse(string data, ref TemplateElementContainer parent)
        {
            var regex = @"call\(\s*(?<property>.*)\s*\)";

            var match = Regex.Match(data, regex);
            if (!match.Success)
                throw new InvalidOperationException($"Unable to parse call epxression: {data}");

            var newCall = new CallElement(parent)
            {
                TypeReference = match.Groups["property"].Value
            };
            parent.Children.Add(newCall);
        }
    }
}
