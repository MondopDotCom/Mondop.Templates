using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondop.Templates.Parsers;

namespace Mondop.Templates.Test
{
    [TestClass]
    public class TemplateParserTests
    {
        private TemplateParser _templateParser;

        [TestInitialize]
        public void TestInitialize()
        {
            _templateParser = new TemplateParser();
        }

        [TestMethod]
        public void ParseTemplate()
        {
            var templateData = TestTemplates.TestTemplate;

            var result = _templateParser.Parse(templateData);

            result.Should().NotBeNull();
            result.Input.Type.Should().Be("Mondop.CodeDom.CompileUnit");
            result.Input.Alias.Should().Be("compileUnit");
            result.Name.Should().Be("TestTemplate");
        }
    }
}
