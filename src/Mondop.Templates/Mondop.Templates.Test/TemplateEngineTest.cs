﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondop.Templates.Parsers;
using Mondop.Templates.Processing;
using Mondop.Templates.Test.Model;

namespace Mondop.Templates.Test
{
    [TestClass]
    public class TemplateEngineTest
    {
        private TemplateEngine _templateEngine;
        private TemplateParser _templateParser;
        private TemplateFactory _templateFactory;


        [TestInitialize]
        public void TestInitialize()
        {
            _templateFactory = new TemplateFactory();
            _templateEngine = new TemplateEngine(_templateFactory, new TestOutputResolver());
            _templateParser = new TemplateParser();
        }

        [TestMethod]
        public void TestTemplate()
        {
            AddTemplate(TestTemplates.TestEngineTemplate);
            AddTemplate(TestTemplates.TestEngineTemplate_TestClassB);

            var outputWriter = _templateEngine.Process(new TestClassA
            {
                Name = "Mondop",
                BClasses = new TestClassB[] { new TestClassB { Name = "One" }, new TestClassB { Name = "Two" } }
            });

            outputWriter.Output.Should().Be("Hello Mondop\r\nHello One\r\nHello Two");
        }

        private void AddTemplate(string templateData)
        {
            var template = _templateParser.Parse(templateData);
            template.Should().NotBeNull();

            _templateFactory.Register(template);
        }
    }
}
