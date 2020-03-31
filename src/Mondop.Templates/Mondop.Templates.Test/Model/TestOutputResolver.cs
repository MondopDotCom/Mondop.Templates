using Mondop.Templates.Model;
using Mondop.Templates.Processing;

namespace Mondop.Templates.Test.Model
{
    public class TestOutputResolver : IOutputResolver
    {
        public IOutputWriter GetWriter(Template template, object input)
        {
            return new TextOutputWriter();
        }
    }
}
