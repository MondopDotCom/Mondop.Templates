using Mondop.Templates.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mondop.Templates.Processing
{
    public interface ITemplateFactory
    {
        void Register(Template template);
        Template GetTemplate(object input);
    }

    public class TemplateFactory: ITemplateFactory
    {
        private readonly List<Template> _registrations = new List<Template>();

        public void Register(Template template)
        {
            _registrations.Add(template);
        }
        public Template GetTemplate(object input)
        {
            return FindTemplate(input.GetType());
        }

        private Template FindTemplate(Type type)
        {
            var matches = MatchByType(type);
            if (matches.Length > 1)
                throw new InvalidOperationException($"Multiple templates found for type: {type}." +
                    $"Found templates: {string.Join("", matches.Select(t => t.Name))}");

            if (matches.Length == 0)
                return FindTemplate(type.BaseType);

            return matches[0];
        }

        private Template[] MatchByType(Type type)
        {
            return _registrations.Where(r => r.Input.ToType() == type).ToArray();
        }
    }
}
