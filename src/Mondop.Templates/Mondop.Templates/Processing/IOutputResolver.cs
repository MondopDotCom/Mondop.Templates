using Mondop.Templates.Model;

namespace Mondop.Templates.Processing
{
    public interface IOutputResolver
    {
        IOutputWriter GetWriter(Template template, object input);
    }
}
