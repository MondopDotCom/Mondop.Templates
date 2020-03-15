using System;
using System.Collections.Generic;
using System.Text;

namespace Mondop.Templates.Model
{
    public class MemberReference : TemplateElement
    {
        public MemberReference(TemplateElement parent) : base(parent)
        {

        }

        public string Member { get; set; }
    }
}
