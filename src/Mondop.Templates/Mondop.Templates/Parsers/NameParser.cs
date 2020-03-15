using Mondop.Templates.Model;
using System;
using System.Text.RegularExpressions;

namespace Mondop.Templates.Parsers
{
    public class NameParser: IKeywordParser
    {
        public void Parse(string data, ref TemplateElementContainer parent)
        {
            var regex = @"name\s(?<name>.*)";

            var match = Regex.Match(data, regex);
            if (!match.Success)
                throw new InvalidOperationException($"Unable to parse name epxression: {data}");

            var newName = new NameElement(parent)
            {
                Name = match.Groups["name"].Value,
            };
            parent.Children.Add(newName);
        }
    }
}
