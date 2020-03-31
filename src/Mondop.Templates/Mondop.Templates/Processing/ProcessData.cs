using System.Collections.Generic;

namespace Mondop.Templates.Processing
{
    internal class ProcessData
    {
        public TemplateEngine TemplateEngine { get; set; }
        public IOutputWriter OutputWriter { get; set; }
        public Dictionary<string, object> InputPath { get; } = new Dictionary<string, object>();
    }
}
