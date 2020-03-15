using Mondop.Templates.Model;
using System;
using System.IO;

namespace Mondop.Templates.Parsers
{
    public class TemplateParser
    {
        public Template ParseFromFile(string fileName)
        {
            var templateData = ReadData(fileName);
            return Parse(templateData);
        }

        public Template Parse(string templateData)
        {
            var template = new Template();
            ParseData(templateData, template);
            return template;
        }

        private string ReadData(string fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                return streamReader.ReadToEnd();
            }
        }

        private void ParseData(string templateData, TemplateElementContainer template)
        {
            const string tagStart = "<@";
            const string tagEnd = "@>";

            var index = 0;
            var parent = template;
            while (index < templateData.Length)
            {
                var s = templateData.IndexOf(tagStart, index);
                var e = s;

                if (s < 0)
                {
                    var literal = new LiteralText(parent)
                    {
                        Text = templateData.Substring(index).Trim('\n', '\r')
                    };
                    if (!string.IsNullOrWhiteSpace(literal.Text))
                        parent.Children.Add(literal);
                    index = templateData.Length;
                    continue;
                }

                if (s >= 0 && s > index)
                {
                    var literal = new LiteralText(parent)
                    {
                        Text = templateData.Substring(index, s - index).Trim('\n','\r')
                    };

                    if (!string.IsNullOrWhiteSpace(literal.Text))
                        parent.Children.Add(literal);
                }

                e = templateData.IndexOf(tagEnd, s);
                if (e == -1)
                    throw new InvalidOperationException($"Missing end {tagEnd}");

                var tagData = templateData.Substring(s + tagStart.Length, e - s - tagStart.Length).Trim();
                ParseTagData(tagData, ref parent);

                index = e + tagEnd.Length;
            }
        }

        private void ParseTagData(string tagData, ref TemplateElementContainer parent)
        {
            if (tagData.StartsWith("#"))
            {
                var memberReference = new MemberReference(parent)
                {
                    Member = tagData.Substring(1)
                };
                parent.Children.Add(memberReference);
                return;
            }
            else if (tagData.StartsWith("//"))
            {
                // Ignore comment
                return;
            }

            var keyword = GetKeyWord(tagData);
            switch (keyword)
            {
                case KeyWords.Input:
                    new InputParser().Parse(tagData, ref parent);
                    break;
                case KeyWords.Name:
                    new NameParser().Parse(tagData, ref parent);
                    break;
                case KeyWords.ForEach:
                    new ForEachParser().Parse(tagData, ref parent);
                    break;
                case KeyWords.Call:
                    new CallParser().Parse(tagData, ref parent);
                    break;
                case KeyWords.End:
                    new EndParser().Parse(tagData, ref parent);
                    break;
                case KeyWords.If:
                case KeyWords.Else:

                case KeyWords.Case:
                case KeyWords.When:
                case KeyWords.Default:
                    break;
                default:
                    throw new InvalidOperationException($"Keyword: {keyword} not implemented.");
            }
        }

        private string GetKeyWord(string tagData)
        {
            var index = tagData.IndexOfAny(new[] { ' ', '(' });
            if (index == -1)
                return tagData;

            return tagData.Substring(0, index);
        }
    }
}
